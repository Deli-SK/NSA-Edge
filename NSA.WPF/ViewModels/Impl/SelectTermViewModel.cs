using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

using NSA.WPF.Common;
using NSA.WPF.Common.Notifications;
using NSA.WPF.Models.Business;
using NSA.WPF.Models.Data;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(ISelectTermViewModel))]
    [Export("Term", typeof(ISelectNodeViewModel))]
    public class SelectTermViewModel : NotifiableBase, ISelectTermViewModel, ISelectNodeViewModel
    {
        private readonly IGraphModel _graphModel;
        private readonly NotifiableProperty<TermNode> _selectedTerm;

        public ICommand ClearSelection { get; }

        public ReadOnlyObservableCollection<TermNode> Terms => this._graphModel.Terms;

        public TermNode SelectedTerm { get { return this._selectedTerm; } set { this._selectedTerm.Value = value; } }

        public Node SelectedNode => this.SelectedTerm;

        [ImportingConstructor]
        public SelectTermViewModel(
            [Import] IGraphModel graphModel)
        {
            this._graphModel = graphModel;

            this._selectedTerm = new NotifiableProperty<TermNode>(this, nameof(this.SelectedTerm));

            this.ClearSelection = new RelayCommand(this.ClearCommand_OnExecute);
        }

        private void ClearCommand_OnExecute(object obj)
        {
            this.SelectedTerm = null;
        }
    }
}