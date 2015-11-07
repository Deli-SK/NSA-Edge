using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using NSA.WPF.Views.Rendering.Engine.Entities;

namespace NSA.WPF.Views.Rendering.Engine
{
    public class Engine2D
    {
        private readonly List<IDrawable> _drawables = new List<IDrawable>();
        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();

        public Engine2D()
        {
            
        }

        public void Add(IUpdatable updatable)
        {
            this._updatables.Add(updatable);
        }

        public void Add(IDrawable drawable)
        {
            this._drawables.Add(drawable);
        }

        public void AddEntity<T>(T entity)
            where T : IDrawable, IUpdatable
        {
            this.Add((IDrawable)entity);
            this.Add((IUpdatable)entity);
        }

        public void Draw(DrawingContext context)
        {
            foreach (var drawable in this._drawables)
            {
                drawable.Draw(context);
            }
        }

        public IEnumerable<IDrawable> GetHit(Point p)
        {
            return this._drawables.Where(d => d.HitTest(p));
        }

        public bool HitTest(Point p)
        {
            return this._drawables.Any(d => d.HitTest(p));
        }

        public bool Remove(object o)
        {
            bool result = false;

            var drawable = o as IDrawable;
            if (drawable != null)
                result = this._drawables.Remove(drawable);
            
            var updatable = o as IUpdatable;
            if (updatable != null)
                result = this._drawables.Remove(drawable) || result;
            
            return result;
        }

        public void Update(double delta)
        {
            foreach (var updatable in this._updatables)
            {
                updatable.Update(delta);
            }
        }
    }
}
