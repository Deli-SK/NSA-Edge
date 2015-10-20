using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using NSA.WPF.Rendering.Engine;
using NSA.WPF.Rendering.Engine.Entities;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Media.Animation;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Pen = System.Windows.Media.Pen;
using Point = System.Windows.Point;

namespace NSA.WPF.Controls
{
    public class EngineControl : Canvas
    {
        private readonly DispatcherTimer _dispatcherTimer;
        private readonly List<IDrawable> _selected = new List<IDrawable>();

        private Vector _offset = new Vector();
        private double _scale = 1;
        private Point _lastMouseLocation;
        private IDrawable _hit;
        private Particle _hoveredElement;

        private DateTime _hitTime;

        public TimeSpan UpdateInterval { get; set; } = new TimeSpan(0, 0, 0, 0, 10);
        public Engine2D Engine2D { get; set; }

        public EngineControl()
        {
            this._dispatcherTimer = new DispatcherTimer();
            this._dispatcherTimer.Tick += this.Tick;
            this._dispatcherTimer.Interval = this.UpdateInterval;
            this.Background = Brushes.White;
        }

        public EngineControl(Engine2D engine2D)
            : this()
        {
            this.Engine2D = engine2D;
        }

        public void Start()
        {
            this._dispatcherTimer.Start();
        }

        public void Stop()
        {
            this._dispatcherTimer.Stop();
        }

        private void Tick(object sender, EventArgs e)
        {
            this.Engine2D?.Update(this.UpdateInterval.TotalSeconds);
            this._hoveredElement = this.Engine2D?.GetHit(this.GetWorldPosition(this._lastMouseLocation)).FirstOrDefault() as Particle;
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var translate = new TranslateTransform(this._offset.X, this._offset.Y);
            var scale = new ScaleTransform(this._scale, this._scale);

            drawingContext.PushTransform(scale);
            drawingContext.PushTransform(translate);

            Brush b = new RadialGradientBrush(Colors.Cyan, Colors.Transparent);
            foreach (var drawable in this._selected)
            {
                drawingContext.DrawEllipse(b, null, drawable.Bounds.BottomRight - (Vector)drawable.Bounds.Size / 2, 50, 50);
            }
            
            this.Engine2D?.Draw(drawingContext);

            if (this._selected.Any())
            {
                Rect selectedRect = Rect.Empty;
                foreach (var drawable in this._selected)
                {
                    selectedRect.Union(drawable.Bounds);
                }
                drawingContext.DrawRectangle(null, new Pen(Brushes.DarkGray, 1) { DashStyle = DashStyles.Dash }, selectedRect);
            }

            if (this._hoveredElement != null)
            {
                var text = new FormattedText(this._hoveredElement.Data.ToString(), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 16, Brushes.Black);


                var origin = this._hoveredElement.Center + new Vector(12, 12);
                var padding = new Vector(5, 5);

                drawingContext.DrawRoundedRectangle(Brushes.LightYellow, new Pen(Brushes.Black, 2),
                    new Rect(origin - padding, new Vector(text.Width, text.Height) + padding * 2), padding.X, padding.Y);
                drawingContext.DrawText(text, origin);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this._lastMouseLocation = e.GetPosition(this);
                this._hit = this.Engine2D.GetHit(this.GetWorldPosition(e)).FirstOrDefault();
                if (this._hit != null)
                {
                    this._hitTime = DateTime.Now;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var position = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var hit = this._hit as IParticle;
                if (hit != null)
                {
                    var worldPosition = this.GetWorldPosition(e);
                    hit.AddForce((worldPosition - hit.Center) / 10);
                }
                else
                {
                    this._offset += (position - this._lastMouseLocation) / this._scale;
                    this.InvalidateVisual();
                }
            }
            this._lastMouseLocation = position;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            var hit = this.Engine2D.GetHit(this.GetWorldPosition(e)).FirstOrDefault();
            if (hit == this._hit)
            {
                if ((DateTime.Now - this._hitTime).TotalSeconds < 0.5f)
                {
                    if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift))
                    {
                        this._selected.Clear();
                    }

                    this._selected.Add(hit);
                }
                return;
            }
            this._selected.Clear();
            this._hit = null;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            this._scale += e.Delta / 1000d;
        }

        private Point GetWorldPosition(MouseEventArgs e)
        {
            return this.GetWorldPosition(e.GetPosition(this));
        }

        private Point GetWorldPosition(Point point)
        {
            return new Point(point.X / this._scale - this._offset.X, point.Y / this._scale - this._offset.Y);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                foreach (var drawable in this._selected)
                {
                    this.Engine2D.Remove(drawable);
                }
                this._selected.Clear();
            }
        }
    }
}
