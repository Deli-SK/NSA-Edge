using System.Windows;
using System.Windows.Media;
using NSA.WPF.Views.Rendering.Engine.Entities.Impl;

namespace NSA.WPF.Views.Rendering.Engine.Entities
{
    public interface IRenderer<in TEntity>
    {
        Rect GetBounds(Particle particle);
        bool HitTest(TEntity particle, Point point);
        void Render(TEntity entity, DrawingContext context);
    }
}
