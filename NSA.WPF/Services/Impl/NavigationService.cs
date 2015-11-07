using System.ComponentModel.Composition;
using System.Windows;

using NSA.WPF.Views.UserControls;
using NSA.WPF.Views.Windows;

namespace NSA.WPF.Services
{
    [Export(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        private Window _termsWindow;
        private Window _sentencesWindow;
        private Window _connectionsWindow;

        public void NavigateToTerms(object dataContext)
        {
            if (this._termsWindow == null || !this._termsWindow.IsLoaded)
                this._termsWindow = new TermsWindow();

            this._termsWindow.DataContext = dataContext;
            this._termsWindow.Show();
            this._termsWindow.Focus();
        }

        public void NavigateToSentences(object dataContext)
        {
            if (this._sentencesWindow == null || !this._sentencesWindow.IsLoaded)
                this._sentencesWindow = new SentencesWindow();

            this._sentencesWindow.DataContext = dataContext;
            this._sentencesWindow.Show();
            this._sentencesWindow.Focus();
        }

        public void NavigateConnections(object dataContext)
        {
            if (this._connectionsWindow == null || !this._connectionsWindow.IsLoaded)
                this._connectionsWindow = new ConnectionsWindow();

            this._connectionsWindow.DataContext = dataContext;
            this._connectionsWindow.Show();
            this._connectionsWindow.Focus();
        }

        public void CloseApplication()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Close();
            }
        }
    }
}