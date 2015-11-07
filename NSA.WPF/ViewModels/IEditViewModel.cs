using System.Windows.Input;
using NSA.WPF.Annotations;

namespace NSA.WPF.ViewModels
{
    public interface IEditViewModel
    {
        [UsedImplicitly]
        ICommand Undo { get; }

        [UsedImplicitly]
        ICommand Redo { get; }
    }
}
