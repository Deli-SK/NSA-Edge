using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Rendering.Engine.Entities.Impl
{
    public class ParticleDrawer: IRenderer<IParticle>
    {
        private readonly Brush _fill;
        private readonly double _radius;
        private readonly Pen _stroke;

        public ParticleDrawer(Brush fill, Pen stroke, double radius)
        {
            this._fill = fill;
            this._stroke = stroke;
            this._radius = radius;
        }

        public Rect GetBounds(Particle particle)
        {
            return new Rect(particle.Center.X - this._radius, particle.Center.Y - this._radius, this._radius * 2, this._radius * 2);
        }

        public bool HitTest(IParticle particle, Point point)
        {
            return (point - particle.Center).LengthSquared < (this._radius * this._radius);
        }

        public void Render(IParticle entity, DrawingContext context)
        {
            context.DrawEllipse(this._fill, this._stroke, entity.Center, this._radius, this._radius);
        }
    }
}
