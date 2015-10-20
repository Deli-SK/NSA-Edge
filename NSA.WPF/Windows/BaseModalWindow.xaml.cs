using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NSA.WPF.Windows
{
    /// <summary>
    /// Interaction logic for BaseModalWindow.xaml
    /// </summary>
    public partial class BaseModalWindow : Window
    {
        public BaseModalWindow()
        {
            this.InitializeComponent();
        }

        public BaseModalWindow(UserControl control)
            : this()
        {
            this.Content.Children.Add(control);
            control.Focus();
        }

        private void CommandBinding_Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void AlwaysExecuteCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
