using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public class EdgedSpringDrawer : IRenderer<Spring>
    {
        private readonly Pen _stroke;
        private readonly Pen _arrow;

        private readonly double _radius;

        public EdgedSpringDrawer(Pen arrow, Pen line, double radius)
        {
            this._arrow = arrow;
            this._stroke = line;
            this._radius = radius;
        }

        public void Render(Spring entity, DrawingContext context)
        {
            var direction = entity.B.Center - entity.A.Center;
            direction.Normalize();

            var offset = direction * this._radius;
            
            context.DrawLine(this._stroke, entity.A.Center + offset, entity.B.Center - offset);
            var arrowOffset = direction * this._arrow.Thickness / 2;

            context.DrawLine(this._arrow, entity.B.Center - offset - arrowOffset, entity.B.Center - offset - arrowOffset + direction * 0.01d);

        }

        public bool HitTest(Spring particle, Point point)
        {
            return false;
        }

        public Rect GetBounds(Particle particle)
        {
            return Rect.Empty;
        }
    }
}
