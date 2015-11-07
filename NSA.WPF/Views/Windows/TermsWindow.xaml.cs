using System.Windows;

namespace NSA.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for TermsWindow.xaml
    /// </summary>
    public partial class TermsWindow
    {
        public TermsWindow()
        {
            this.InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.TermTextBox.SelectAll();
            this.TermTextBox.Focus();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
