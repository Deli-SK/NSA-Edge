using System;
using System.Windows;

using NSA.WPF.Common;
using NSA.WPF.ViewModels;
using NSA.WPF.Views.Windows;

namespace NSA.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Composition.RegisterAssembly(typeof(App).Assembly);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.RegisterResources();
            this.OpenMainWindow();
        }

        private void OpenMainWindow()
        {
            var window = new MainWindow
            {
                DataContext = Composition.Resolve<IMainWindowViewModel>()
            };

            this.MainWindow = window;
            this.MainWindow.Show();
        }

        private void RegisterResources()
        {
            this.Resources.MergedDictionaries.Add(
                LoadComponent(new Uri(@"/Views/Themes/Default.xaml", UriKind.Relative)) as ResourceDictionary);
        }
    }
}
