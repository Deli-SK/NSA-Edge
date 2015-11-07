using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using System.Windows.Media.Animation;
using NSA.WPF.Common;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IEditViewModel))]
    public class EditViewModel : IEditViewModel
    {
        private readonly IStateService _stateService;
        private readonly RelayCommand _undo;
        private readonly RelayCommand _redo;

        public ICommand Undo => this._undo;
        public ICommand Redo => this._redo;

        [ImportingConstructor]
        public EditViewModel(
            [Import] IStateService stateService)
        {
            this._stateService = stateService;

            this._undo = new RelayCommand(this.UndoCommand_OnExecute, this.UndoCommand_CanExecute);
            this._redo = new RelayCommand(this.RedoCommand_OnExecute, this.RedoCommand_CanExecute);

            this._stateService.RedoCalled += this.StateService_OnChanged;
            this._stateService.UndoCalled += this.StateService_OnChanged;
            this._stateService.ActionRegistered += this.StateService_OnChanged;
        }

        private void StateService_OnChanged(object sender, EventArgs eventArgs)
        {
            this._undo.Refresh();
            this._redo.Refresh();
        }

        private bool UndoCommand_CanExecute(object obj)
        {
            return this._stateService.CanUndo();
        }

        private void UndoCommand_OnExecute(object obj)
        {
            this._stateService.Undo();
        }

        private bool RedoCommand_CanExecute(object obj)
        {
            return this._stateService.CanRedo();
        }

        private void RedoCommand_OnExecute(object obj)
        {
            this._stateService.Redo();
        }
    }
}