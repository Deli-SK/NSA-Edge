using System.IO;
using System.Windows.Input;
using System.ComponentModel.Composition;

using NSA.WPF.Common;
using NSA.WPF.Common.Extensions;
using NSA.WPF.Models.Business;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IFileViewModel))]
    public class FileViewModel : IFileViewModel
    {
        private readonly IGraphModel _graphModel;

        private readonly INavigationService _navigationService;
        private readonly IFileProviderService _openFileProviderService;
        private readonly IFileProviderService _saveFileProviderService;
        private readonly IMessageService _messageService;
        private readonly IPromptService _promptService;

        private FileInfo _fileInfo;

        public ICommand New { get; }
        public ICommand Open { get; }
        public ICommand Save { get; }
        public ICommand SaveAs { get; }
        public ICommand Exit { get; }

        [ImportingConstructor]
        public FileViewModel([Import] IGraphModel graphModel,
            [Import] INavigationService navigationService,
            [Import("OpenFile")] IFileProviderService openFileProviderService,
            [Import("SaveFile")] IFileProviderService saveFileProviderService,
            [Import] IMessageService messageService, 
            [Import] IPromptService promptService)
        { 
            this.New = new RelayCommand(this.NewCommand_Execute);
            this.Open = new RelayCommand(this.OpenCommand_Execute);
            this.Save = new RelayCommand(this.SaveCommand_Execute);
            this.SaveAs = new RelayCommand(this.SaveAsCommand_Execute);
            this.Exit = new RelayCommand(this.ExitCommand_Execute);

            this._graphModel = graphModel;

            this._navigationService = navigationService;
            this._openFileProviderService = openFileProviderService;
            this._saveFileProviderService = saveFileProviderService;
            this._messageService = messageService;
            this._promptService = promptService;
        }

        private void ExitCommand_Execute(object obj)
        {
            if (this._promptService.Prompt("Do you really want to quit the application?") == true)
            {
                if (this._fileInfo != null)
                {
                    if (this._promptService.Prompt($"Do you want to save the '{this._fileInfo.Name}'?") == true)
                    {
                        this.Save.Execute(null);
                    }
                }
                this._navigationService.CloseApplication();
            }
        }

        private void NewCommand_Execute(object obj)
        {
            this._fileInfo = null;
            this._graphModel.Clear();
        }

        private void OpenCommand_Execute(object obj)
        {
            if (this._fileInfo != null)
            {
                if (this._promptService.Prompt($"Do you want to save the '{this._fileInfo.Name}'?") == true)
                {
                    this.Save.Execute();
                }
            }

            this._fileInfo = this._openFileProviderService.Execute();

            if (this._fileInfo != null)
            {
                if (!this._fileInfo.Exists)
                {
                    this._messageService.ShowError($"File '{this._fileInfo.Name}' not found!");
                    return;
                }

                using (var stream = this._fileInfo.Open(FileMode.Open))
                {
                    if (!this._graphModel.LoadFrom(stream))
                    {
                        this._messageService.ShowError($"File '{this._fileInfo.Name}' could not be loaded.");
                        this._fileInfo = null;
                    }
                }
            }
        }

        private void SaveAsCommand_Execute(object obj)
        {
            var fileInfo = this._saveFileProviderService.Execute();
            if (fileInfo != null)
            {
                this._fileInfo = fileInfo;
                this.Save.Execute();
            }
        }

        private void SaveCommand_Execute(object obj)
        {
            if (this._fileInfo != null)
            {
                using (var stream = this._fileInfo.Open(FileMode.OpenOrCreate))
                {
                    if (!this._graphModel.SaveTo(stream))
                    {
                        this._messageService.ShowError($"There was an error saving to '{this._fileInfo.Name}'");
                        this._fileInfo = null;
                    }
                }
            }
            else
            {
                this.SaveAs.Execute();
            }
        }
    }
}