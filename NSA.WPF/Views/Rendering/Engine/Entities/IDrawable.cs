using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Rendering.Engine.Entities
{
    public interface IDrawable
    {
        Rect Bounds { get; }
        void Draw(DrawingContext context);
        bool HitTest(Point p);
    }
}