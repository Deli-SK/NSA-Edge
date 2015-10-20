using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddTermControl.xaml
    /// </summary>
    public partial class AddTermControl
    {
        private readonly IGraphFacade _facade;

        public AddTermControl()
        {
            this.InitializeComponent();
        }

        public AddTermControl(IGraphFacade facade) 
            : this()
        {
            this._facade = facade;
        }


        private void CommandBinding_AddTerm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var term = this.Term?.Text;
            e.CanExecute = !string.IsNullOrEmpty(term) && !this._facade.ContainsTerm(term);
        }

        private void CommandBinding_AddTerm_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var term = this.Term?.Text;

            if (term == null)
                throw new ArgumentException($"'{e.Parameter}' is not a string");

            this._facade.AddTerm(term);

            this.Term.SelectAll();
            this.Term.Focus();
        }

        private void Term_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.Term.SelectAll();
        }
    }
}
