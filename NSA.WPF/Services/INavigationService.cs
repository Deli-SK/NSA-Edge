using System.Windows.Input;

namespace NSA.WPF.Services
{
    public interface INavigationService
    {
        void NavigateToTerms(object dataContext);
        void NavigateToSentences(object dataContext);
        void NavigateConnections(object dataContext);
        void CloseApplication();
    }
}