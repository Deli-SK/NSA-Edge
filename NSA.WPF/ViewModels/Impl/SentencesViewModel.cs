using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

using NSA.WPF.Common;
using NSA.WPF.Models.Business;
using NSA.WPF.Services;

namespace NSA.WPF.ViewModels
{
    [Export(typeof(ISentencesViewModel))]
    public class SentencesViewModel : ISentencesViewModel
    {
        private readonly IGraphModel _graphModel;
        private readonly IStateService _stateService;
        private readonly RelayCommand _addSentence;
        private uint? _chapter;
        private uint? _sentence;

        public ICommand AddSentence => this._addSentence;
        public ICommand Clear { get; }

        public uint? Chapter
        {
            get { return this._chapter; }
            set { this._chapter = value; this._addSentence.Refresh(); }
        }

        public uint? Sentence
        {
            get { return this._sentence; }
            set { this._sentence = value; this._addSentence.Refresh(); }
        }

        [ImportingConstructor]
        public SentencesViewModel(
            [Import]IGraphModel graphModel,
            [Import]IStateService stateService)
        {
            this._graphModel = graphModel;
            this._stateService = stateService;

            this.Clear = new RelayCommand(this.ClearCommand_OnExecute);
            this._addSentence = new RelayCommand(this.AddSentenceCommand_OnExecute, this.AddSentenceCommand_CanExecute);
        }

        private bool AddSentenceCommand_CanExecute(object obj)
        {
            return
                this._chapter.HasValue && this._chapter.Value > 0
                && this._sentence.HasValue && this._sentence.Value > 0
                && this._graphModel.Sentences.All(
                        node =>
                            !Equals(this._chapter, node.Chapter) 
                            || !Equals(this._sentence, node.Sentence));
        }

        private void AddSentenceCommand_OnExecute(object obj)
        {
            if (!this._chapter.HasValue || !this._sentence.HasValue)
                throw new ArgumentOutOfRangeException();

            var node = this._graphModel.AddSentence(this._chapter.Value, this._sentence.Value);
            this._addSentence.Refresh();
            
            this._stateService.RegisterAction(
                () => this._graphModel.RemoveSentence(node),
                () => this._graphModel.AddSentence(node.Chapter, node.Sentence));
        }

        private void ClearCommand_OnExecute(object obj)
        {
            this._chapter = null;
            this._sentence = null;
        }
    }
}