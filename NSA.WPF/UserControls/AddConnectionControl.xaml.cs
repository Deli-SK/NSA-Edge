using System;
using System.Windows.Controls;
using System.Windows.Input;
using NSA.WPF.Business.Data;
using NSA.WPF.Business.Common;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddTermControl.xaml
    /// </summary>
    public partial class AddConnectionControl
    {
        private readonly IGraphFacade _facade;

        private uint Page
        {
            get
            {
                uint result;
                return uint.TryParse(this.PagesComboBox?.Text, out result)
                    ? result
                    : 0;
            }
        }

        private uint Sentence
        {
            get
            {
                uint result;
                return uint.TryParse(this.SentencesComboBox?.Text, out result)
                    ? result
                    : 0;
            }
        }

        public ConnectionType ConnectionType { get; set; }

        public AddConnectionControl()
        {
            this.InitializeComponent();
        }

        public AddConnectionControl(IGraphFacade facade) 
            : this()
        {
            this._facade = facade;

            this.DataContext = this;

            this.UpdateTermsListBox();
            this.UpdatePagesListBox();
            this.UpdateSentencesListBox(0);
        }

        private void UpdateTermsListBox()
        {
            this.TermsComboBox.Items.Clear();
            foreach (var term in this._facade.GetTerms())
            {
                this.TermsComboBox.Items.Add(term);
            }
        }

        private void UpdatePagesListBox()
        {
            this.PagesComboBox.Items.Clear();
            foreach (var page in this._facade.GetPages())
            {
                this.PagesComboBox.Items.Add(page);
            }
        }

        private void UpdateSentencesListBox(uint page)
        {
            var any = false;
            this.SentencesComboBox.Items.Clear();

            if (page > 0)
            {
                foreach (var sentence in this._facade.GetSentences(page))
                {
                    any = true;
                    this.SentencesComboBox.Items.Add(sentence);
                }
            }

            this.SentencesComboBox.IsEnabled = any;
        }

        private void PagesComboBox_OnSelected(object sender, SelectionChangedEventArgs e)
        {
            uint page = 0;
            if (e.AddedItems.Count > 0)
            {
                page = e.AddedItems[0] as uint? ?? 0;
            }
            this.UpdateSentencesListBox(page);
        }

        private void CommandBinding_Connect_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var term = this.TermsComboBox?.Text;

            var page = this.Page;
            var sentence = this.Sentence;

            e.CanExecute = page > 0 && sentence > 0 && !String.IsNullOrEmpty(term)
                           && this._facade.ContainsSentence(page, sentence)
                           && this._facade.ContainsTerm(term)
                           && !this._facade.HasConnection(page, sentence, term);
        }

        private void CommandBinding_Connect_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var term = this.TermsComboBox?.Text;

            this._facade.Connect(this.Page, this.Sentence, term, this.ConnectionType);
        }

        private void CommandBinding_Disconnect_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var term = this.TermsComboBox?.Text;

            var page = this.Page;
            var sentence = this.Sentence;

            e.CanExecute = page > 0 && sentence > 0 && !String.IsNullOrEmpty(term)
                           && this._facade.ContainsSentence(page, sentence)
                           && this._facade.ContainsTerm(term)
                           && this._facade.HasConnection(page, sentence, term);
        }

        private void CommandBinding_Disconnect_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var term = this.TermsComboBox?.Text;

            this._facade.Disconnect(this.Page, this.Sentence, term);
        }

        private void ConnectionTypeComboBox_OnInitialized(object sender, EventArgs e)
        {
            this.ConnectionTypeComboBox.ItemsSource = new[] { ConnectionType.Containing, ConnectionType.Explaining };
            this.ConnectionType = ConnectionType.Containing;
        }
    }
}
