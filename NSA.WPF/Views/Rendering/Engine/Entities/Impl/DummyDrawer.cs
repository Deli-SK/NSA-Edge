using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Rendering.Engine.Entities.Impl
{
    public class DummyDrawer: IRenderer<IDrawable>
    {
        public Rect GetBounds(Particle particle)
        {
            return Rect.Empty;
        }

        public bool HitTest(IDrawable particle, Point point)
        {
            return false;
        }

        public void Render(IDrawable entity, DrawingContext context)
        {
            
        }
    }
}
