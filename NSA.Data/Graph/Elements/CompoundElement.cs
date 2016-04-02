using System;
using System.Collections.Generic;
using System.Linq;

namespace NSA.Data.Graph.Elements
{
    public abstract class CompoundElement
    {
        private readonly List<object> _components = new List<object>();

        public T GetComponent<T>()
            where T : class
        {
            return this.GetComponents<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetComponents<T>()
            where T : class
        {
            return this._components.OfType<T>();
        }

        public T AddComponent<T>(T component)
        {
            this._components.Add(component);
            return component;
        }

        public void RemoveComponent<T>(T component)
        {
            this._components.Remove(component);
        }

        public void RemoveComponent<T>(Predicate<T> match)
        {
            this._components.RemoveAll(m => m is T && match((T) m));
        }
    }
}
