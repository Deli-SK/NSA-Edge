using System.Windows;

namespace NSA.Visualization.Elements
{
    public class Anchor : IRigidBody
    {
        public Point Position { get; }

        public Anchor(Point position)
        {
            this.Position = position;
        }

        public void AddForce(Vector force) { }
    }
}