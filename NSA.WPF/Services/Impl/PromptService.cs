using System;
using System.ComponentModel.Composition;
using System.Windows;

namespace NSA.WPF.Services
{
    [Export(typeof(IPromptService))]
    public class PromptService : IPromptService
    {
        public bool? Prompt(string message)
        {
            switch (MessageBox.Show(message, "", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation))
            {
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    return null;
                case MessageBoxResult.OK:
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}