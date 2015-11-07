using System.Windows.Input;
using NSA.WPF.Models.Data;

namespace NSA.WPF.ViewModels
{
    public interface ISelectNodeViewModel
    {
        Node SelectedNode { get; }

        ICommand ClearSelection { get; }
    }
}