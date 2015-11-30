using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using NSA.WPF.Common.Cacheing;
using NSA.WPF.Models.Business;
using NSA.WPF.Models.Data;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(IGraphViewModel))]
    public class GraphViewModel : IGraphViewModel
    {
        private IGraphModel _graphModel;

        public ReadOnlyObservableCollection<Connection> Edges => this._graphModel.Connections;
        public ReadOnlyObservableCollection<TermNode> Terms => this._graphModel.Terms;
        public ReadOnlyObservableCollection<SentenceNode> Sentences => this._graphModel.Sentences;
        
        [ImportingConstructor]
        public GraphViewModel(
            [Import] IGraphModel graphModel,
            [Import] IGraphLayoutingService layoutingService)
        {
            this._graphModel = graphModel;

            var nodes = new CachedValue<List<Node>>(this.GetNodes);

            layoutingService.AttachNodeSource(nodes);
        }

        private List<Node> GetNodes()
        {
            var list = new List<Node>(this.Terms.Count + this.Sentences.Count);
            list.AddRange(this.Terms);
            list.AddRange(this.Sentences);
            return list;
        }
    }
}