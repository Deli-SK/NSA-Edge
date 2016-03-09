using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;
using NSA.WPF.Models.Data;
using NSA.WPF.Models.Serialization;

namespace NSA.WPF.Models.Business
{
    [Export(typeof(IGraphModel))]
    public class GraphModel : IGraphModel
    {
        private readonly IGraphSerializer _serializer;

        private readonly ObservableCollection<TermNode> _termNodes = new ObservableCollection<TermNode>();
        private readonly ObservableCollection<SentenceNode> _sentenceNodes = new ObservableCollection<SentenceNode>();
        private readonly ObservableCollection<Connection> _connections = new ObservableCollection<Connection>();

        public ReadOnlyObservableCollection<TermNode> Terms { get; }
        public ReadOnlyObservableCollection<SentenceNode> Sentences { get; }
        public ReadOnlyObservableCollection<Connection> Connections { get; }

        [ImportingConstructor]
        public GraphModel(
            [Import] IGraphSerializer serializer)
        {
            this._serializer = serializer;

            this.Terms = new ReadOnlyObservableCollection<TermNode>(this._termNodes);
            this.Sentences = new ReadOnlyObservableCollection<SentenceNode>(this._sentenceNodes);
            this.Connections = new ReadOnlyObservableCollection<Connection>(this._connections);
        }

        public SentenceNode AddSentence(uint chapter, uint sentence)
        {
            var node = new SentenceNode(chapter, sentence);
            this._sentenceNodes.Add(node);
            return node;
        }

        public TermNode AddTerm(string term)
        {
            var node = new TermNode(term);
            this._termNodes.Add(node);
            return node;
        }

        public Connection AddConnection(Node from, Node to)
        {
            var connection = new Connection(from, to);
            this._connections.Add(connection);
            return connection;
        }

        public void Clear()
        {
            this._termNodes.Clear();
            this._sentenceNodes.Clear();
            this._connections.Clear();
        }

        public bool LoadFrom(Stream stream)
        {
            TermNode[] terms;
            SentenceNode[] sentences;
            Connection[] connections;

            if (this._serializer.Deserialize(stream, out terms, out sentences, out connections))
            {
                this._termNodes.Clear();
                this._sentenceNodes.Clear();
                this._connections.Clear();

                foreach (var termNode in terms)
                    this._termNodes.Add(termNode);

                foreach (var sentenceNode in sentences)
                    this._sentenceNodes.Add(sentenceNode);

                foreach (var connection in connections)
                    this._connections.Add(connection);

                return true;
            }
            return false;
        }

        public bool RemoveConnection(Connection connection)
        {
            return this._connections.Remove(connection);
        }

        public bool RemoveSentence(SentenceNode node)
        {
            return this._sentenceNodes.Remove(node) && this.RemoveConnectionsWith(node);
        }

        public bool RemoveTerm(TermNode node)
        {
            return this._termNodes.Remove(node) && this.RemoveConnectionsWith(node);
        }

        public bool SaveTo(Stream stream)
        {
            return this._serializer.Serialize(stream, this);
        }

        private bool RemoveConnectionsWith(Node node)
        {
            var toDelete = this._connections.Where(c => c.From == node || c.To == node).ToArray();

            foreach (var item in toDelete)
            {
                this._connections.Remove(item);
            }

            return toDelete.Any();
        }
    }
}