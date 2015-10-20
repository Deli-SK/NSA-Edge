using System;
using System.Windows;
using System.Windows.Input;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddTermControl.xaml
    /// </summary>
    public partial class AddSentenceControl
    {
        private readonly IGraphFacade _facade;

        private uint Page
        {
            get
            {
                uint result;
                return uint.TryParse(this.PageInput?.Text, out result)
                    ? result
                    : 0;
            }
        }

        private uint Sentence
        {
            get
            {
                uint result;
                return uint.TryParse(this.SentenceInput?.Text, out result)
                    ? result
                    : 0;
            }
        }


        public AddSentenceControl()
        {
            this.InitializeComponent();
        }

        public AddSentenceControl(IGraphFacade facade) 
            : this()
        {
            this._facade = facade;
        }


        private void CommandBinding_AddSentence_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var page = this.Page;
            var sentence = this.Sentence;

            e.CanExecute = page > 0 && sentence > 0 && !this._facade.ContainsSentence(page, sentence);
        }

        private void CommandBinding_AddSentence_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var page = this.Page;
            var sentence = this.Sentence;

            if (page == 0)
                throw new ArgumentException($"'{this.PageInput.Text}' is not a valid number");

            if (sentence == 0)
                throw new ArgumentException($"'{this.SentenceInput.Text}' is not a valid number");

            this._facade.AddSentence(page, sentence);

            this.SentenceInput.SelectAll();
            this.SentenceInput.Focus();

        }

        private void Page_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.PageInput.SelectAll();
        }

        private void Sentence_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.SentenceInput.SelectAll();
        }
    }
}
