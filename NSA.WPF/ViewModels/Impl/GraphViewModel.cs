using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

using NSA.WPF.Models.Data;
using Point = System.Windows.Point;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IGraphViewModel))]
    public class GraphViewModel : IGraphViewModel
    {
        public ReadOnlyObservableCollection<object> Edges { get; }
        public ReadOnlyObservableCollection<object> Nodes { get; }

        private ReadOnlyObservableCollection<object> GetNodes()
        {
            return new ReadOnlyObservableCollection<object>(new ObservableCollection<object>(new object[]
            {
                new TermNode("Test"), new TermNode("Was"), new TermNode("Here"),
                new SentenceNode(1, 1), new SentenceNode(1, 2), new SentenceNode(2, 8), new Point(15, 120), 42
            }));
        }

        private void Tick()
        {
        }
    }
}