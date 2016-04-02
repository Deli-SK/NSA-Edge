using System.Collections.Generic;
using NSA.Data.Graph.Elements;

namespace NSA.Data.Graph
{
    public class GraphEngine
    {
        private readonly List<Node> _nodes = new List<Node>();
        private readonly List<Connection> _connections = new List<Connection>();

        public void AddNode(Node node)
        {
            this._nodes.Add(node);
        }

        public void RemoveNode(Node node)
        {
            this._nodes.Remove(node);
            this._nodes.ForEach(n => n.Disconnect(node));
            this._connections.RemoveAll(c => c.SourceNode == node || c.TargetNode == node);
        }

        public Connection AddConnection(Node source, Node target)
        {
            var connection = source.Connect(target);
            this._connections.Add(connection);
            return connection;
        }

        public void RemoveConnection(Node source, Node target)
        {
            var connection = source.Disconnect(target);
            this._connections.Remove(connection);
        }

        public void RemoveConnection(Connection connection)
        {
            connection.SourceNode.Disconnect(connection.TargetNode);
            this._connections.Remove(connection);
        }
    }
}
