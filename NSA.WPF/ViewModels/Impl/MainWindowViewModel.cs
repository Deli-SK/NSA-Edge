using System.ComponentModel.Composition;
using System.Windows.Input;

using NSA.WPF.Common;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IMainWindowViewModel))]
    public sealed class MainWindowViewModel : IMainWindowViewModel
    {
        public string Title { get; } = "NSA: Network System Analysis Tool";

        [Import]
        public IGraphViewModel Graph { get; set; }

        [Import]
        public IEditViewModel Edit { get; set; }

        [Import]
        public IFileViewModel File { get; set; }

        [Import]
        public IConnectionsViewModel Connections { get; set; }

        [Import]
        public ISentencesViewModel Sentences { get; set; }

        [Import]
        public ITermsViewModel Terms { get; set; }

        public ICommand NavigateToTerms { get; }
        public ICommand NavigateToSentences { get; }
        public ICommand NavigateToConnections { get; }

        [ImportingConstructor]
        public MainWindowViewModel(
            [Import] INavigationService navigationService)
        {
            this.NavigateToTerms = new RelayCommand(navigationService.NavigateToTerms);
            this.NavigateToSentences = new RelayCommand(navigationService.NavigateToSentences);
            this.NavigateToConnections = new RelayCommand(navigationService.NavigateConnections);
        }
    }
}
