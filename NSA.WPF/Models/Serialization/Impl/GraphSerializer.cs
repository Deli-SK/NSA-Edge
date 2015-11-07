using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using NSA.WPF.Models.Business;
using NSA.WPF.Models.Data;

namespace NSA.WPF.Models.Serialization
{
    [Export(typeof(IGraphSerializer))]
    public class GraphSerializer : IGraphSerializer
    {
        private const string VERSION = "1.0";

        public bool Serialize(Stream stream, IGraphModel model)
        {
            try
            {
                uint id = 0;
                var cache = new Dictionary<Node, uint>();

                var terms = new XElement("Terms");

                foreach (var termNode in model.Terms)
                {
                    terms.Add(new XElement("TermNode",
                        new XAttribute("Id", id),
                        new XAttribute("Term", termNode.Term)));

                    cache.Add(termNode, id);
                    id++;
                }

                var sentences = new XElement("Sentences");

                foreach (var sentenceNode in model.Sentences)
                {
                    sentences.Add(new XElement("SentenceNode",
                        new XAttribute("Id", id),
                        new XAttribute("Chapter", sentenceNode.Chapter),
                        new XAttribute("Sentence", sentenceNode.Sentence)));

                    cache.Add(sentenceNode, id);
                    id++;
                }

                var connections = new XElement("Connections");

                foreach (var connection in model.Connections)
                {
                    uint fromId;
                    uint toId;

                    cache.TryGetValue(connection.From, out fromId);
                    cache.TryGetValue(connection.To, out toId);

                    connections.Add(new XElement("Connection",
                        new XAttribute("From", fromId),
                        new XAttribute("To", toId)));
                }

                var root = new XElement("NSA.Save",
                    new XAttribute("Version", VERSION),
                    terms,
                    sentences,
                    connections);

                root.Save(stream);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Deserialize(Stream stream, out TermNode[] terms, out SentenceNode[] sentences, out Connection[] connections)
        {
            
            terms = null;
            sentences = null;
            connections = null;

            try
            {
                var document = XDocument.Load(stream);


                var termsList = new List<TermNode>();
                var sentencesList = new List<SentenceNode>();
                var connectionList = new List<Connection>();

                Dictionary<uint, Node> cache = new Dictionary<uint, Node>();

                var root = document.Root;

                if (root == null || root.Name != "NSA.Save" || root.Attribute("Version")?.Value != VERSION)
                {
                    return false;
                }

                var termElements = root.Elements("Terms").SelectMany(e => e.Elements());
                var sentenceElements = root.Elements("Sentences").SelectMany(e => e.Elements());
                var connectionElements = root.Elements("Connections").SelectMany(e => e.Elements());

                foreach (var termElement in termElements)
                {
                    var id = uint.Parse(termElement.Attribute("Id").Value);
                    var term = termElement.Attribute("Term").Value;

                    var node = new TermNode(term);

                    termsList.Add(node);
                    cache.Add(id, node);
                }

                foreach (var sentenceElement in sentenceElements)
                {
                    var id = uint.Parse(sentenceElement.Attribute("Id").Value);
                    var chapter = uint.Parse(sentenceElement.Attribute("Chapter").Value);
                    var sentence = uint.Parse(sentenceElement.Attribute("Sentence").Value);

                    var node = new SentenceNode(chapter, sentence);

                    sentencesList.Add(node);
                    cache.Add(id, node);
                }

                foreach (var connectionElement in connectionElements)
                {
                    var from = uint.Parse(connectionElement.Attribute("From").Value);
                    var to = uint.Parse(connectionElement.Attribute("To").Value);

                    var connection = new Connection(cache[from], cache[to]);

                    connectionList.Add(connection);
                }

                terms = termsList.ToArray();
                sentences = sentencesList.ToArray();
                connections = connectionList.ToArray();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}