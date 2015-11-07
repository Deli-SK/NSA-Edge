using System.Collections.Generic;
using System.ComponentModel;
using NSA.WPF.Annotations;
using NSA.WPF.Models.Data;

namespace NSA.WPF.ViewModels
{
    public interface ISelectSentenceViewModel : INotifyPropertyChanged
    {
        [UsedImplicitly]
        IEnumerable<uint> Chapters { get; }

        [UsedImplicitly]
        IEnumerable<SentenceNode> Sentences { get; }

        [UsedImplicitly]
        uint? SelectedChapter { get; set; }

        [UsedImplicitly]
        SentenceNode SelectedSentence { get; set; }
    }
}
