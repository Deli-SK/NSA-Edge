using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

using NSA.WPF.Common;
using NSA.WPF.Common.Notifications;
using NSA.WPF.Models.Business;
using NSA.WPF.Models.Data;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(ISelectSentenceViewModel))]
    [Export("Sentence", typeof(ISelectNodeViewModel))]
    public class SelectSentenceViewModel : NotifiableBase, ISelectSentenceViewModel, ISelectNodeViewModel
    {
        private readonly IGraphModel _graphModel;
        private readonly NotifiableProperty<uint?> _selectedChapter;
        private readonly NotifiableProperty<SentenceNode> _selectedSentence;

        public IEnumerable<uint> Chapters => this.GetChapters();
        public IEnumerable<SentenceNode> Sentences => this.GetSentences();

        public uint? SelectedChapter { get { return this._selectedChapter; } set { this._selectedChapter.Value = value; } }
        public SentenceNode SelectedSentence { get { return this._selectedSentence; } set { this._selectedSentence.Value = value; } }

        public Node SelectedNode => this.SelectedSentence;

        public ICommand ClearSelection { get; }

        [ImportingConstructor]
        public SelectSentenceViewModel(
            [Import] IGraphModel graphModel)
        {
            this._graphModel = graphModel;

            this._selectedChapter = new NotifiableProperty<uint?>(this, nameof(this.Sentences));
            this._selectedSentence = new NotifiableProperty<SentenceNode>(this, nameof(this.SelectedSentence), nameof(this.SelectedNode));

            ((INotifyCollectionChanged)this._graphModel.Sentences).CollectionChanged += (sender, args) => { Console.WriteLine(args.NewItems?[0]);};
            this.ClearSelection = new RelayCommand(this.ClearCommand_OnExecute);
        }

        private void ClearCommand_OnExecute(object obj)
        {
            this.SelectedSentence = null;
            this.SelectedChapter = null;
        }

        private IEnumerable<uint> GetChapters()
        {
            return this._graphModel.Sentences.Select(s => s.Chapter).Distinct();
        }

        private IEnumerable<SentenceNode> GetSentences()
        {
            if (!this.SelectedChapter.HasValue)
                return new SentenceNode[0];

            return this._graphModel.Sentences
                .Where(s => Equals(s.Chapter, this.SelectedChapter));
        }
    }
}