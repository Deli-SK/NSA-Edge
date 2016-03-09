using System.Windows;
using System.Windows.Media;

namespace NSA.WPF.Views.Controls
{
    public class EdgeControl: FrameworkElement
    {
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(float), typeof(EdgeControl), new FrameworkPropertyMetadata(ForceRender));
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Point), typeof(EdgeControl), new FrameworkPropertyMetadata(ForceRender));
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Point), typeof(EdgeControl), new FrameworkPropertyMetadata(ForceRender));

        public float Radius
        {
            get { return (float)this.GetValue(RadiusProperty); }
            set { this.SetValue(RadiusProperty, value); this.InvalidateVisual(); }
        }

        public Point From
        {
            get { return (Point) this.GetValue(FromProperty); }
            set { this.SetValue(FromProperty, value); this.InvalidateVisual(); }
        }

        public Point To
        {
            get { return (Point) this.GetValue(ToProperty); }
            set { this.SetValue(ToProperty, value); this.InvalidateVisual(); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var from = this.From;
            var to = this.To;

            var direction = to - from;
            direction.Normalize();

            from += direction * this.Radius;
            to -= direction * this.Radius;

            var directionNormal = new Vector(direction.Y, - direction.X);

            var thickness = 4;

            var a = to - direction * thickness * 4 + directionNormal * thickness * 2;
            var b = to - direction * thickness * 4 - directionNormal * thickness * 2;

            var pen = new Pen(Brushes.Magenta, thickness);

            drawingContext.DrawLine(pen, from, to - direction * thickness * 4);

            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(to, true, true);
                geometryContext.LineTo(a, true, false);
                geometryContext.LineTo(b, true, false);
            }

            drawingContext.DrawGeometry(Brushes.Magenta, null, geometry);
        }

        private static void ForceRender(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EdgeControl)d).InvalidateVisual();
        }
    }
}
