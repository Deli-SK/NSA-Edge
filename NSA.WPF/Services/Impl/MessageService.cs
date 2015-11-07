using System.ComponentModel.Composition;
using System.Windows;

namespace NSA.WPF.Services
{
    [Export(typeof(IMessageService))]
    public class MessageService : IMessageService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}