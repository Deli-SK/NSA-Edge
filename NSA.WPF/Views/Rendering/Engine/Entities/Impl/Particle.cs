using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Rendering.Engine.Entities.Impl
{
    public class Particle: IParticle, IUpdatable, IDrawable
    {
        private const double FRICTION_MODIFIER = 0.9d;
        private readonly IRenderer<Particle> _renderer;

        private Vector _speed;

        public Rect Bounds => this._renderer.GetBounds(this);
        public object Data { get; }
        public Point Center { get; private set; }

        public Particle(IRenderer<Particle> renderer, Point center, object data)
        {
            this.Center = center;
            this._renderer = renderer;
            this.Data = data;

            this._speed = new Vector(0, 0);
        }

        public void AddForce(Vector force)
        {
            this._speed += force;
        }

        public void Draw(DrawingContext context)
        {
            this._renderer.Render(this, context);
        }

        public bool HitTest(Point p)
        {
            return this._renderer.HitTest(this, p);
        }

        public void Update(double delta)
        {
            this.Center += this._speed * delta;
            this._speed *= FRICTION_MODIFIER;
        }
    }
}
