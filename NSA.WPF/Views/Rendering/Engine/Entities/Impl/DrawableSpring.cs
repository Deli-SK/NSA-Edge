using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Rendering.Engine.Entities.Impl
{
    public class DrawableSpring : Spring, IDrawable
    {
        public Rect Bounds => Rect.Empty;
        public IRenderer<DrawableSpring> Renderer { get; set; }

        public DrawableSpring(IRenderer<DrawableSpring> renderer, IParticle a, IParticle b, double length, double strength = 0.5) 
            : base(a, b, length, strength)
        {
            this.Renderer = renderer;
        }

        public void Draw(DrawingContext context)
        {
            this.Renderer?.Render(this, context);
        }

        public bool HitTest(Point p)
        {
            return this.Renderer?.HitTest(this, p) ?? false;
        }
    }
}