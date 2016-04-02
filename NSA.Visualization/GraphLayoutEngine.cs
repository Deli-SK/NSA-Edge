using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using NSA.Data.Graph.Elements;
using NSA.Visualization.Elements;

namespace NSA.Visualization
{
    public class GraphLayoutEngine: IUpdatable
    {
        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        
        public void Tick(float delay)
        {
            this._updatables.ForEach(u => u.Tick(delay));
        }

        public void Attach(Node n)
        {
            var fromRB = n.GetComponent<RigidBody>() ?? n.AddComponent(new RigidBody(new Point()));

            if (!this._updatables.Contains(fromRB))
                this._updatables.Add(fromRB);

            foreach (var connection in n.GetConnections())
            {
                this.Attach(connection);
            }
        }

        private void Attach(Connection connection)
        {
            var fromRB = connection.SourceNode.GetComponent<RigidBody>() 
                ?? connection.SourceNode.AddComponent(new RigidBody(new Point())); ;
            var toRB = connection.TargetNode.GetComponent<RigidBody>() 
                ?? connection.TargetNode.AddComponent(new RigidBody(new Point()));

            var spring = connection.GetComponent<Spring>() ?? connection.AddComponent(new Spring(fromRB, toRB, 100));

            if (!this._updatables.Contains(spring))
                this._updatables.Add(spring);
        }

        public void Detach(Node n)
        {
            //TODO:
        }
    }
}
