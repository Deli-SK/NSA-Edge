using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using NSA.WPF.Rendering.Engine.Entities;

namespace NSA.WPF.Rendering.Engine
{
    public class Engine2D
    {
        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        private readonly List<IDrawable> _drawables = new List<IDrawable>();

        public Engine2D()
        {
            
        }

        public void Update(double delta)
        {
            foreach (var updatable in this._updatables)
            {
                updatable.Update(delta);
            }
        }

        public void Draw(DrawingContext context)
        {
            foreach (var drawable in this._drawables)
            {
                drawable.Draw(context);
            }
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

        public bool HitTest(Point p)
        {
            return this._drawables.Any(d => d.HitTest(p));
        }

        public IEnumerable<IDrawable> GetHit(Point p)
        {
            return this._drawables.Where(d => d.HitTest(p));
        }
    }
}
