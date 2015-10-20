using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public class ParticleDrawer: IRenderer<IParticle>
    {
        private readonly Brush _fill;
        private readonly Pen _stroke;
        private readonly double _radius;

        public ParticleDrawer(Brush fill, Pen stroke, double radius)
        {
            this._fill = fill;
            this._stroke = stroke;
            this._radius = radius;
        }

        public void Render(IParticle entity, DrawingContext context)
        {
            context.DrawEllipse(this._fill, this._stroke, entity.Center, this._radius, this._radius);
        }

        public bool HitTest(IParticle particle, Point point)
        {
            return (point - particle.Center).LengthSquared < (this._radius * this._radius);
        }

        public Rect GetBounds(Particle particle)
        {
            return new Rect(particle.Center.X - this._radius, particle.Center.Y - this._radius, this._radius * 2, this._radius * 2);
        }
    }
}
