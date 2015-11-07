using System;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.Win32;

namespace NSA.WPF.Services
{
    [Export("OpenFile", typeof(IFileProviderService))]
    public class OpenFileProviderService: IFileProviderService
    {
        private readonly Lazy<OpenFileDialog> _openFileDialog;

        public OpenFileProviderService()
        {
            this._openFileDialog = new Lazy<OpenFileDialog>(this.CreateOpenDialog);
        }

        public FileInfo Execute()
        {
            var dialog = this._openFileDialog.Value;
            if (dialog.ShowDialog() == true)
            {
                return new FileInfo(dialog.FileName);
            }
            return null;
        }

        private OpenFileDialog CreateOpenDialog()
        {
            return new OpenFileDialog()
            {
                Filter = "NSA Files|*.nsa|All Files|*.*"
            };
        }
    }
}