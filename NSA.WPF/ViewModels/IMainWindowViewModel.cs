using System.Windows.Input;
using NSA.WPF.Annotations;

namespace NSA.WPF.ViewModels
{
    public interface IMainWindowViewModel
    {
        [UsedImplicitly]
        string Title { get; }

        [UsedImplicitly]
        IConnectionsViewModel Connections { get; }

        [UsedImplicitly]
        ISentencesViewModel Sentences { get; }

        [UsedImplicitly]
        ITermsViewModel Terms { get; }

        [UsedImplicitly]
        IEditViewModel Edit { get; }

        [UsedImplicitly]
        IFileViewModel File { get; }

        [UsedImplicitly]
        IGraphViewModel Graph { get; }

        [UsedImplicitly]
        ICommand NavigateToTerms { get; }

        [UsedImplicitly]
        ICommand NavigateToSentences { get; }

        [UsedImplicitly]
        ICommand NavigateToConnections { get; }
    }
}