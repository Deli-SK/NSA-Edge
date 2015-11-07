using System;
using System.Windows.Input;

namespace NSA.WPF.Common
{
    public class RelayCommand<T>: ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<T> _onExecute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> onExecute)
        {
            this._onExecute = onExecute;
        }

        public RelayCommand(Action<T> onExecute, Predicate<T> canExecute)
        {
            this._onExecute = onExecute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            this._onExecute((T)parameter);
        }

        public void Refresh()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> onExecute) : base(onExecute)
        {
        }

        public RelayCommand(Action<object> onExecute, Predicate<object> canExecute) : base(onExecute, canExecute)
        {
        }
    }
}
