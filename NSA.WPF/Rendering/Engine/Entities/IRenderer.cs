using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public interface IRenderer<in TEntity>
    {
        void Render(TEntity entity, DrawingContext context);
        bool HitTest(TEntity particle, Point point);
        Rect GetBounds(Particle particle);
    }
}
