using System.Collections.Generic;
using System.Linq;

namespace NSA.Data.Graph.Elements
{
    public class Node: CompoundElement
    {
        private readonly List<Connection> _connections = new List<Connection>();

        public IReadOnlyList<Connection> GetConnections()
        {
            return this._connections;
        } 

        public Connection GetConnection(Node target)
        {
            return this._connections.FirstOrDefault(c => c.TargetNode == target);
        }

        internal Connection Connect(Node target)
        {
            if (this.GetConnection(target) == null)
            {
                var connection = new Connection(this, target);
                this._connections.Add(connection);
                return connection;
            }
            return null;
        }

        internal Connection Disconnect(Node target)
        {
            var connection = this.GetConnection(target);
            if (connection != null)
                this._connections.Remove(connection);
            return connection;
        }
    }
}
