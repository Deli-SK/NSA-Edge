using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using NSA.WPF.Business.Common;
using NSA.WPF.Business.Common.Actions;

namespace NSA.WPF.Business.Facades.Impl
{
    public class ContentAwareGraphFacade : IContentAwareGraphFacade
    {
        private readonly IGraphFacade _graphFacade;

        private readonly Stack<GraphAction> _undoActions = new Stack<GraphAction>();

        private readonly Stack<GraphAction> _redoActions = new Stack<GraphAction>();


        public event Action Change
        {
            add { this._graphFacade.Change += value; }
            remove { this._graphFacade.Change -= value; }
        }

        public ContentAwareGraphFacade(IGraphFacade graphFacade)
        {
            this._graphFacade = graphFacade;
        }

        public void Clear()
        {
            this._graphFacade.Clear();
            this._undoActions.Clear();
            this._redoActions.Clear();
        }

        public void Save(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(GraphAction[]));
            serializer.WriteObject(stream, this._undoActions.ToArray());
        }

        public bool Load(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(GraphAction[]));
            var graphActions = serializer.ReadObject(stream) as GraphAction[];

            if (graphActions == null)
                return false;

            this.Clear();

            foreach (var graphAction in graphActions)
            {
                this._redoActions.Push(graphAction);
            }

            while (this.CanRedo())
            {
                this.Redo();
            }

            return true;
        }

        public bool CanUndo()
        {
            return this._undoActions.Any();
        }

        public bool CanRedo()
        {
            return this._redoActions.Any();
        }

        public void Undo()
        {
            var action = this._undoActions.Pop();
            this._redoActions.Push(action);

            action.Undo(this._graphFacade);
        }

        public void Redo()
        {
            var action = this._redoActions.Pop();
            this._undoActions.Push(action);

            action.Do(this._graphFacade);
        }

        public bool AddSentence(uint page, uint sentence)
        {
            return this.DoAction(new AddSentenceAction(page, sentence));
        }

        public bool AddTerm(string term)
        {
            return this.DoAction(new AddTermAction(term));
        }

        public bool Connect(uint page, uint sentence, string term, ConnectionType type)
        {
            return this.DoAction(new AddConnectionAction(page, sentence, term, type));
        }

        public bool RemoveSentence(uint page, uint sentence)
        {
            return this.DoAction(new InverseAction(new AddSentenceAction(page, sentence)));
        }

        public bool RemoveTerm(string term)
        {
            return this.DoAction(new InverseAction(new AddTermAction(term)));
        }

        public bool Disconnect(uint page, uint sentence, string term)
        {
            return this.DoAction(new InverseAction(new AddConnectionAction(page, sentence, term, ConnectionType.None)));
        }

        public bool ContainsTerm(string term)
        {
            return this._graphFacade.ContainsTerm(term);
        }

        public bool ContainsSentence(uint page, uint sentence)
        {
            return this._graphFacade.ContainsSentence(page, sentence);
        }

        public bool HasConnection(uint page, uint sentence, string term)
        {
            return this._graphFacade.HasConnection(page, sentence, term);
        }

        public IEnumerable<string> GetTerms()
        {
            return this._graphFacade.GetTerms();
        }

        public IEnumerable<uint> GetPages()
        {
            return this._graphFacade.GetPages();
        }

        public IEnumerable<uint> GetSentences(uint page)
        {
            return this._graphFacade.GetSentences(page);
        }

        private bool DoAction(GraphAction action)
        {
            if (action.Do(this._graphFacade))
            {
                this._undoActions.Push(action);
                return true;
            }
            return false;
        }
    }
}