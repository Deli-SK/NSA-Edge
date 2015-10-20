using System;
using System.Collections.Generic;
using System.Linq;
using NSA.WPF.Business.Common;
using NSA.WPF.Business.Data.Nodes;
using NSA.WPF.Rendering.Graph;

namespace NSA.WPF.Business.Facades.Impl
{
    public sealed class GraphFacade : IGraphFacade
    {
        public event Action Change;
        
        private readonly Dictionary<string, TermNode> _terms = new Dictionary<string, TermNode>();
        private readonly Dictionary<string, SentenceNode> _sentences = new Dictionary<string, SentenceNode>();

        private GraphLayoutEngine _layout;

        // TODO: Decoupling
        public GraphFacade(GraphLayoutEngine engine)
        {
            this._layout = engine;
        }

        public bool AddTerm(string term)
        {
            this.AddTerm(new TermNode(term));
            return true;
        }

        public bool AddSentence(uint page, uint sentence)
        {
            this.AddSentence(new SentenceNode(page, sentence));
            return true;
        }

        public IEnumerable<string> GetTerms()
        {
            return this._terms.Values.Select(n => n.Term);
        }

        public IEnumerable<uint> GetPages()
        {
            return this._sentences.Values.Select(node => node.Page).Distinct();
        }

        public IEnumerable<uint> GetSentences(uint page)
        {
            return this._sentences.Values.Where(node => node.Page == page).Select(node => node.Sentence);
        }

        public bool RemoveTerm(string term)
        {
            if (this._terms.Remove(TermNode.GetLabel(term)))
            {
                this._layout.Remove(TermNode.GetLabel(term));
                this.OnChange();
                return true;
            }

            return false;
        }

        public bool RemoveSentence(uint page, uint sentence)
        {
            if (this._sentences.Remove(SentenceNode.GetLabel(page, sentence)))
            {
                this._layout.Remove(SentenceNode.GetLabel(page, sentence));

                this.OnChange();
                return true;
            }
            return false;
        }

        public bool ContainsTerm(string term)
        {
            return this._terms.ContainsKey(TermNode.GetLabel(term));
        }

        public bool ContainsSentence(uint page, uint sentence)
        {
            return this._sentences.ContainsKey(SentenceNode.GetLabel(page, sentence));
        }

        public bool Connect(uint page, uint sentence, string term, ConnectionType type)
        {
            var termNode = this.GetTermNode(term);
            var sentenceNode = this.GetSentenceNode(page, sentence);

            if (termNode == null || sentenceNode == null)
            {
                return false;
            }

            sentenceNode.AddConnection(termNode, type);
            this._layout.Connect(sentenceNode.ToString(), termNode.ToString(), type);

            this.OnChange();
            return true;
        }

        public bool HasConnection(uint page, uint sentence, string term)
        {
            var termNode = this.GetTermNode(term);
            var sentenceNode = this.GetSentenceNode(page, sentence);

            return termNode != null
                   && sentenceNode != null
                   && sentenceNode.HasConnection(termNode);

        }

        public bool Disconnect(uint page, uint sentence, string term)
        {
            var termNode = this.GetTermNode(term);
            var sentenceNode = this.GetSentenceNode(page, sentence);

            if (termNode == null || sentenceNode == null)
            {
                return false;
            }

            if (sentenceNode.RemoveConnection(termNode))
            {
                this.OnChange();
                this._layout.Disconnect(sentenceNode.ToString(), termNode.ToString());
                return true;
            }

            return false;
        }

        public void Clear()
        {
            var changed = this._terms.Any() || this._sentences.Any();

            this._terms.Clear();
            this._sentences.Clear();
            this._layout.Clear();

            if (changed)
                this.OnChange();
        }

        private void AddTerm(TermNode term)
        {
            this._terms.Add(term.ToString(), term);
            this._layout.AddTerm(term.ToString());
            this.OnChange();
        }

        private void AddSentence(SentenceNode sentence)
        {
            this._sentences.Add(sentence.ToString(), sentence);
            this._layout.AddSentence(sentence.ToString());
            this.OnChange();
        }

        private TermNode GetTermNode(string term)
        {
            TermNode node;

            this._terms.TryGetValue(TermNode.GetLabel(term), out node);
            return node;
        }

        private SentenceNode GetSentenceNode(uint page, uint sentence)
        {
            SentenceNode node;

            this._sentences.TryGetValue(SentenceNode.GetLabel(page, sentence), out node);
            return node;
        }

        private void OnChange()
        {
            this.Change?.Invoke();
        }
    }
}
