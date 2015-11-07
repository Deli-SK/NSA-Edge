using System.ComponentModel;
using System.Runtime.CompilerServices;
using NSA.WPF.Annotations;

namespace NSA.WPF.Common.Notifications
{
    public interface INotifiable: INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName = null);
    }

    public abstract class NotifiableBase : INotifiable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
