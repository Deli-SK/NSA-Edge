using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using NSA.WPF.Common.Cacheing;
using NSA.WPF.Models.Data;

namespace NSA.WPF.Services
{
    [Export(typeof(IGraphLayoutingService))]
    public class GraphLayoutingService : IGraphLayoutingService
    {
        private static readonly Point CENTER = new Point(300, 300);
        private static readonly float MIN_NODE_DISTANCE = 200;
        private static readonly float MAX_CENTER_OFFSET = 300;
        private static readonly float CONNECTED_DISTANCE = 100;
        
        private IValueProvider<Node[]> _nodes;
        private IValueProvider<Connection[]> _connections;

        public GraphLayoutingService()
        {
            var timer = new DispatcherTimer();
            timer.Tick += (sender, args) => this.Tick();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
        }

        public void AttachNodeSource(IValueProvider<Node[]> nodes)
        {
            this._nodes = nodes;
        }

        public void AttachConnectionSource(IValueProvider<Connection[]> connections)
        {
            this._connections = connections;
        }

        private void Tick()
        {
            var nodes = this._nodes.Value;
            var edges = this._connections.Value;

            var nodeForces = new Vector[nodes.Length];

            for (int i = 0; i < nodes.Length; i++)
            {
                var from = nodes[i];
                nodeForces[i] += this.ForcePull(from.Position, CENTER, MAX_CENTER_OFFSET, 0.01f);
                
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    var to = nodes[j];

                    if (edges.Any(e => e.From == from && e.To == to)
                        || edges.Any(e => e.From == to && e.To == from))
                    {
                        nodeForces[i] += this.ForcePull(from.Position, to.Position, CONNECTED_DISTANCE, 0.1f);
                        nodeForces[i] += this.ForcePush(from.Position, to.Position, CONNECTED_DISTANCE, 0.1f);

                        nodeForces[j] += this.ForcePull(to.Position, from.Position, CONNECTED_DISTANCE, 0.1f);
                        nodeForces[j] += this.ForcePush(to.Position, from.Position, CONNECTED_DISTANCE, 0.1f);
                    }
                    else
                    {
                        nodeForces[i] += this.ForcePush(from.Position, to.Position, MIN_NODE_DISTANCE, 0.05f);
                        nodeForces[j] += this.ForcePush(to.Position, from.Position, MIN_NODE_DISTANCE, 0.05f);
                    }
                }
            }
            

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Position += nodeForces[i];
            }
        }

        private Vector ForcePush(Point from, Point to, float desiredLength, float strength)
        {
            var direction = to - from;
            var length = direction.Length;
            var offset = length - desiredLength;
            if (offset < 0)
            {
                direction.Normalize();
                return direction * offset * strength;
            }

            return new Vector();
        }

        private Vector ForcePull(Point from, Point to, float desiredLength, float strength)
        {
            var direction = to - from;
            var length = direction.Length;
            var offset = length - desiredLength;
            if (offset > 0)
            {
                direction.Normalize();
                return direction * offset * strength;
            }

            return new Vector();
        }
    }
}