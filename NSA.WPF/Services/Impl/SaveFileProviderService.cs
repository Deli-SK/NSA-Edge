using System;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.Win32;

namespace NSA.WPF.Services
{
    [Export("SaveFile", typeof(IFileProviderService))]
    public class SaveFileProviderService: IFileProviderService
    {
        private readonly Lazy<SaveFileDialog> _saveFileDialog;

        public SaveFileProviderService()
        {
            this._saveFileDialog = new Lazy<SaveFileDialog>(this.CreateSaveDialog);
        }

        public FileInfo Execute()
        {
            var dialog = this._saveFileDialog.Value;
            if (dialog.ShowDialog() == true)
            {
                return new FileInfo(dialog.FileName);
            }
            return null;
        }

        private SaveFileDialog CreateSaveDialog()
        {
            return new SaveFileDialog()
            {
                Filter = "NSA Files|*.nsa|All Files|*.*"
            };
        }
    }
}