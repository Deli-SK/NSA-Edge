using System.Windows;

namespace NSA.Visualization.Elements
{
    public class RigidBody : IRigidBody, IUpdatable
    {
        private Vector _force;
        private Vector _speed;
        public Point Position { get; private set; }

        public RigidBody(Point position)
        {
            this.Position = position;
            this._force = new Vector(0, 0);
            this._speed = new Vector(0, 0);
        }

        public void AddForce(Vector force)
        {
            this._force += force;
        }

        public void Tick(float delay)
        {
            this._speed += this._force;
            this.Position += this._speed;

            this._force = new Vector(0, 0);
        }
    }
}
