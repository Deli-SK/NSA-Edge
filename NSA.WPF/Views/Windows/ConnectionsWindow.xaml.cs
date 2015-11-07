using System.Windows;
using System.Windows.Controls;

namespace NSA.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for ConnectionsWindow.xaml
    /// </summary>
    public partial class ConnectionsWindow
    {
        public ConnectionsWindow()
        {
            this.InitializeComponent();
        }
        

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConnectionType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }

}
