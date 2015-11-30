using System;

namespace NSA.WPF.Common.Cacheing
{
    public class CachedValue<T> : IValueProvider<T>
        where T : class
    {
        private readonly Func<T> _valueFactory;
        private T _value;

        public T Value => this._value ?? (this._value = this._valueFactory());

        public CachedValue(Func<T> valueFactory)
        {
            this._valueFactory = valueFactory;
        }

        public void Clear()
        {
            this._value = null;
        }
    }
}
