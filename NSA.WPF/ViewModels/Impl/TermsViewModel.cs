using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

using NSA.WPF.Common;
using NSA.WPF.Models.Business;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(ITermsViewModel))]
    public sealed class TermsViewModel : ITermsViewModel
    {
        private readonly IGraphModel _graphModel;
        private readonly IStateService _stateService;

        private readonly RelayCommand _addTerm;

        private string _termToAdd;

        public ICommand AddTerm => this._addTerm;
        public ICommand Clear { get; }

        public string TermToAdd
        {
            get { return this._termToAdd; }
            set { this._termToAdd = value; this._addTerm.Refresh(); }
        }

        [ImportingConstructor]
        public TermsViewModel(
            [Import]IGraphModel graphModel,
            [Import]IStateService stateService)
        {
            this._graphModel = graphModel;
            this._stateService = stateService;

            this.Clear = new RelayCommand(this.ClearCommand_OnExecute);
            this._addTerm = new RelayCommand(this.AddTermCommand_OnExecute, this.AddTermCommand_CanExecute);
        }

        private bool AddTermCommand_CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(this.TermToAdd)
                && this._graphModel.Terms.All(node => !Equals(this.TermToAdd, node.Term));
        }

        private void AddTermCommand_OnExecute(object obj)
        {
            var node = this._graphModel.AddTerm(this.TermToAdd);

            this._addTerm.Refresh();

            this._stateService.RegisterAction(
                () => this._graphModel.RemoveTerm(node), 
                () => this._graphModel.AddTerm(node.Term));
        }

        private void ClearCommand_OnExecute(object obj)
        {
            this._termToAdd = String.Empty;
        }
    }
}