using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public class DummyDrawer: IRenderer<IDrawable>
    {
        public void Render(IDrawable entity, DrawingContext context)
        {
            
        }

        public bool HitTest(IDrawable particle, Point point)
        {
            return false;
        }

        public Rect GetBounds(Particle particle)
        {
            return Rect.Empty;
        }
    }
}
