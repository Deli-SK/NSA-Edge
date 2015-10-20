using System.Windows.Controls;
using System.Windows.Input;

namespace NSA.WPF.Commands
{
    public static class CustomCommands
    {
        public static readonly RoutedCommand AddTerm = new RoutedCommand("Add Term", typeof(Control), Keys(Key.T, ModifierKeys.Alt));
        public static readonly RoutedCommand AddSentence = new RoutedCommand("Add Sentence", typeof(Control), Keys(Key.S, ModifierKeys.Alt));
        public static readonly RoutedCommand AddConnection = new RoutedCommand("Add Connection", typeof(Control), Keys(Key.C, ModifierKeys.Alt));
        public static readonly RoutedCommand RemoveConnection = new RoutedCommand("Remove Connection", typeof(Control), Keys(Key.C, ModifierKeys.Alt | ModifierKeys.Shift));
        public static readonly RoutedCommand InverseSelection = new RoutedCommand("Invert Selection", typeof(Control), Keys(Key.I, ModifierKeys.Control));

        private static InputGestureCollection Keys(Key key, ModifierKeys modifiers)
        {
            return new InputGestureCollection { new KeyGesture(key, modifiers) };
        }
    }
}
