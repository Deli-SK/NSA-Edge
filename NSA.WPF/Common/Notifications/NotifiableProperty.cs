using System.Collections.Generic;
using System.Linq;

namespace NSA.WPF.Common.Notifications
{
    public class NotifiableProperty<T>
    {
        private readonly INotifiable _owner;
        private readonly List<string> _notify;
        private T _value;

        public T Value
        {
            get { return this._value; }
            set
            {
                if (Equals(this._value, value)) return;
                this._value = value;
                this.Notify();
            }
        }

        public NotifiableProperty(INotifiable owner, params string[] notify)
        {
            this._owner = owner;
            this._notify = notify.ToList();
        }

        public NotifiableProperty(INotifiable owner, T value = default(T), params string[] notify)
            :this(owner, notify)
        {
            this._value = value;
        }

        public static implicit operator T(NotifiableProperty<T> property)
        {
            return property.Value;
        }

        public void Attach(string notify)
        {
            this._notify.Add(notify);
        }

        public void Detach(string notify)
        {
            this._notify.Remove(notify);
        }

        public bool IsSet()
        {
            return Equals(this.Value, default(T));
        }

        public void Notify()
        {
            foreach (var name in this._notify)
            {
                this._owner.OnPropertyChanged(name);
            }
        }

        public void Reset()
        {
            this.Value = default(T);
        }
    }
}
