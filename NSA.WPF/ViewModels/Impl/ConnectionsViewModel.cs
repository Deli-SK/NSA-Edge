using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

using NSA.WPF.Common;
using NSA.WPF.Models.Business;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IConnectionsViewModel))]
    public class ConnectionsViewModel : IConnectionsViewModel
    {
        private readonly IGraphModel _graphModel;
        private readonly IStateService _stateService;

        private readonly RelayCommand _addConnection;
        private readonly RelayCommand _removeConnection;

        public ISelectSentenceViewModel From { get; }
        public ISelectTermViewModel To { get; }

        public ICommand AddConnection => this._addConnection;
        public ICommand RemoveConnection => this._removeConnection;

        [ImportingConstructor]
        public ConnectionsViewModel(
            [Import] IGraphModel graphModel,
            [Import] IStateService stateService,
            [Import] ISelectSentenceViewModel from,
            [Import] ISelectTermViewModel to)
        {
            this._graphModel = graphModel;
            this._stateService = stateService;

            this.From = from;
            this.To = to;

            this._addConnection = new RelayCommand(this.AddConnectionCommand_OnExecute, this.AddConnectionCommand_CanExecute);
            this._removeConnection = new RelayCommand(this.RemoveConnectionCommand_OnExecute, this.RemoveConnectionCommand_CanExecute);

            this.From.PropertyChanged += this.From_OnpropertyChanged;
            this.To.PropertyChanged += this.To_OnpropertyChanged;

        }

        private bool AddConnectionCommand_CanExecute(object obj)
        {
            var from = this.From.SelectedSentence;
            var to = this.To.SelectedTerm;

            return from != null 
                   && to != null
                   && !this._graphModel.Connections.Any(c => c.From == from && c.To == to);
        }

        private void AddConnectionCommand_OnExecute(object obj)
        {
            var from = this.From.SelectedSentence;
            var to = this.To.SelectedTerm;

            var connection = this._graphModel.AddConnection(from, to);

            this._stateService.RegisterAction(
                () => this._graphModel.RemoveConnection(connection),
                () => this._graphModel.AddConnection(from, to));

            this._addConnection.Refresh();
            this._removeConnection.Refresh();
        }

        private void From_OnpropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.From.SelectedSentence))
            {
                this._addConnection.Refresh();
                this._removeConnection.Refresh();
            }
        }

        private bool RemoveConnectionCommand_CanExecute(object obj)
        {
            var from = this.From.SelectedSentence;
            var to = this.To.SelectedTerm;

            return from != null
                   && to != null
                   && this._graphModel.Connections.Any(c => c.From == from && c.To == to);
        }

        private void RemoveConnectionCommand_OnExecute(object obj)
        {
            var from = this.From.SelectedSentence;
            var to = this.To.SelectedTerm;

            var connection = this._graphModel.Connections.FirstOrDefault(c => c.From == from && c.To == to);

            if (connection != null)
            {
                this._graphModel.RemoveConnection(connection);

                this._stateService.RegisterAction(
                    () => this._graphModel.AddConnection(connection.From, connection.To),
                    () => this._graphModel.RemoveConnection(connection));

            }

            this._addConnection.Refresh();
            this._removeConnection.Refresh();
        }

        private void To_OnpropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.To.SelectedTerm))
            {
                this._addConnection.Refresh();
                this._removeConnection.Refresh();
            }
        }
    }
}