using System.Collections.Generic;
using System.Windows;
using NSA.WPF.Common.Cacheing;
using NSA.WPF.Models.Data;

namespace NSA.WPF.Services
{
    public class GraphLayoutingService : IGraphLayoutingService
    {
        private static readonly Point CENTER = new Point(0, 0);
        private static readonly float MIN_NODE_DISTANCE = 200;
        private static readonly float CONNECTED_DISTANCE = 100;
        
        private IValueProvider<ICollection<Node>> _nodes;
        private IValueProvider<ICollection<Connection>> _connections;

        public void AttachNodeSource(IValueProvider<ICollection<Node>> nodes)
        {
            this._nodes = nodes;
        }

        public void AttachConnectionSource(IValueProvider<ICollection<Connection>> connections)
        {
            this._connections = connections;
        }
    }
}