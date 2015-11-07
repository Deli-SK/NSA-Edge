using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Controls
{
    public class EdgeControl: FrameworkElement
    {
        public Point From { get; set; }

        public Point To { get; set; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawLine(new Pen(Brushes.Magenta, 4), this.From, this.To);
        }

    }
}
