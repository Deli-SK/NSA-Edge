using System.Windows.Input;
using NSA.WPF.Annotations;

namespace NSA.WPF.ViewModels
{
    public interface ITermsViewModel
    {
        [UsedImplicitly]
        string TermToAdd { get; set; }

        [UsedImplicitly]
        ICommand AddTerm { get; }

        [UsedImplicitly]
        ICommand Clear { get; }
    }
}
