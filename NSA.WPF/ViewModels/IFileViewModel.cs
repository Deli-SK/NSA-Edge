using System.Windows.Input;
using NSA.WPF.Annotations;

namespace NSA.WPF.ViewModels
{
    public interface IFileViewModel
    {
        [UsedImplicitly]
        ICommand New { get; }

        [UsedImplicitly]
        ICommand Open { get; }

        [UsedImplicitly]
        ICommand Save { get; }

        [UsedImplicitly]
        ICommand SaveAs { get; }

        [UsedImplicitly]
        ICommand Exit { get; }
    }
}
