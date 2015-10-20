using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public interface IDrawable
    {
        Rect Bounds { get; }
        bool HitTest(Point p);
        void Draw(DrawingContext context);
    }
}