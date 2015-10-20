using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public class Particle: IParticle, IUpdatable, IDrawable
    {
        private const double FRICTION_MODIFIER = 0.9d;
        public Point Center { get; private set; }
        public object Data { get; }

        private Vector _speed;
        private readonly IRenderer<Particle> _renderer; 

        public Particle(IRenderer<Particle> renderer, Point center, object data)
        {
            this.Center = center;
            this._renderer = renderer;
            this.Data = data;

            this._speed = new Vector(0, 0);
        }

        public Rect Bounds => this._renderer.GetBounds(this);

        public bool HitTest(Point p)
        {
            return this._renderer.HitTest(this, p);
        }

        public void Draw(DrawingContext context)
        {
            this._renderer.Render(this, context);
        }
        
        public void AddForce(Vector force)
        {
            this._speed += force;
        }

        public void Update(double delta)
        {
            this.Center += this._speed * delta;
            this._speed *= FRICTION_MODIFIER;
        }
    }
}
