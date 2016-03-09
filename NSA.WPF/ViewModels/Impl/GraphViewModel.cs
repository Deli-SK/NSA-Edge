using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Data;
using NSA.WPF.Annotations;
using NSA.WPF.Common.Cacheing;
using NSA.WPF.Models.Business;
using NSA.WPF.Models.Data;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IGraphViewModel))]
    public class GraphViewModel : IGraphViewModel
    {
        private readonly CompositeCollection _elements = new CompositeCollection();

        private readonly IGraphModel _graphModel;

        public ReadOnlyObservableCollection<Connection> Connections => this._graphModel.Connections;
        public ReadOnlyObservableCollection<TermNode> Terms => this._graphModel.Terms;
        public ReadOnlyObservableCollection<SentenceNode> Sentences => this._graphModel.Sentences;
        public CompositeCollection Elements => this._elements;

        [ImportingConstructor]
        public GraphViewModel(
            [Import] IGraphLayoutingService layoutingService,
            [Import] IGraphModel graphModel)
        {
            this._graphModel = graphModel;

            this._elements.Add(new CollectionContainer{ Collection = this.Connections });
            this._elements.Add(new CollectionContainer{ Collection = this.Terms});
            this._elements.Add(new CollectionContainer{ Collection = this.Sentences });
            
            var nodes = new CachedValue<Node[]>(this.GetNodes);
            var connections = new CachedValue<Connection[]>(() => this.Connections.ToArray());
            
            layoutingService.AttachNodeSource(nodes);
            layoutingService.AttachConnectionSource(connections);

            ((INotifyCollectionChanged)this.Terms).CollectionChanged += (sender, args) => nodes.Clear();
            ((INotifyCollectionChanged)this.Sentences).CollectionChanged += (sender, args) => nodes.Clear();
            ((INotifyCollectionChanged)this.Connections).CollectionChanged += (sender, args) => connections.Clear();

        }

        private Node[] GetNodes()
        {
            var list = new List<Node>(this.Terms.Count + this.Sentences.Count);
            list.AddRange(this.Terms);
            list.AddRange(this.Sentences);
            return list.ToArray();
        }
    }
}