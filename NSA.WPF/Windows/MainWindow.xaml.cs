using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using NSA.WPF.Business.Facades;
using NSA.WPF.Business.Facades.Impl;
using NSA.WPF.Rendering.Engine;
using NSA.WPF.Rendering.Graph;
using NSA.WPF.UserControls;

namespace NSA.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string WindowTitle => $"{this.FileName ?? "Untitled"}{(this.Changed ? "*" : "")} - {Settings.Default.Title}";

        private string FileName { get; set; }
        private bool Changed { get; set; }

        private readonly IContentAwareGraphFacade _graphFacade;

        public MainWindow()
        {
            this.DataContext = this;

            //TODO: use IoC
            var engine2D = new Engine2D();
            var layout = new GraphLayoutEngine(engine2D);
            this._graphFacade = new ContentAwareGraphFacade(new GraphFacade(layout));



            this._graphFacade.Change += this.OnChange;

            ApplicationCommands.Close.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
            ApplicationCommands.SaveAs.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));

            this.InitializeComponent();
            
            this.UpdateTitle();

            this.Canvas.Engine2D = engine2D;
            this.Canvas.Start();
        }

        private void OnChange()
        {
            this.Changed = true;
            this.UpdateTitle();
        }

        private void AlwaysExecuteCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_New_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this._graphFacade.Clear();
            this.FileName = null;
        }
        
        private void CommandBinding_Open_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = Settings.Default.FileFilter,
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Open))
                {
                    bool loaded;
                    try
                    {
                        loaded = this._graphFacade.Load(stream);

                        this.FileName = dialog.FileName;
                        this.Changed = false;

                        this.UpdateTitle();
                    }
                    catch (Exception)
                    {
                        loaded = false;
                    }

                    if (!loaded)
                    {
                        MessageBox.Show(
                            "An Error occured during opening the file. \nPlease make sure that the file is in the correct format.",
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CommandBinding_Save_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Save(this.FileName == null);
        }

        private void CommandBinding_SaveAs_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Save(true);
        }

        private void CommandBinding_Exit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CommandBinding_Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._graphFacade.CanUndo();
        }

        private void CommandBinding_Undo_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this._graphFacade.Undo();
        }

        private void CommandBinding_Redo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._graphFacade.CanRedo();
        }
        
        private void CommandBinding_Redo_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this._graphFacade.Redo();
        }

        private void CommandBinding_AddTerm_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new BaseModalWindow(new AddTermControl(this._graphFacade));
            window.Owner = this;
            window.Title = "Add Term";
            window.Show();
        }

        private void CommandBinding_AddSentence_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new BaseModalWindow(new AddSentenceControl(this._graphFacade));
            window.Owner = this;
            window.Title = "Add Sentence";
            window.Show();
        }

        private void CommandBinding_AddConnection_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new BaseModalWindow(new AddConnectionControl(this._graphFacade));
            window.Owner = this;
            window.Title = "Add/Remove Connection";
            window.Show();
        }
        
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit the application?", Settings.Default.Title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.No:
                    e.Cancel = true;
                    return;

                case MessageBoxResult.OK:
                    break;
            }

            if (this.Changed)
            {
                result = MessageBox.Show($"Would you like to save changes to '{this.WindowTitle}' before quitting?", Settings.Default.Title, MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        return;

                    case MessageBoxResult.Yes:
                        this.Save(this.FileName == null);
                        break;

                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void Save(bool showDialog)
        {
            var dialog = new SaveFileDialog
            {
                FileName = this.FileName,
                Filter = Settings.Default.FileFilter
            };

            if (!showDialog || dialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    this._graphFacade.Save(stream);

                    this.FileName = dialog.FileName;
                    this.Changed = false;

                    this.UpdateTitle();
                }
            }
        }

        private void UpdateTitle()
        {
            this.Title = this.WindowTitle;
        }

    }
}
