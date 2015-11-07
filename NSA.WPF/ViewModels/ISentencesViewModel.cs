using System.Windows.Input;
using NSA.WPF.Annotations;

namespace NSA.WPF.ViewModels
{
    public interface ISentencesViewModel
    {
        [UsedImplicitly]
        uint? Chapter { get; set; }

        [UsedImplicitly]
        uint? Sentence { get; set; }

        [UsedImplicitly]
        ICommand AddSentence { get; }

        [UsedImplicitly]
        ICommand Clear { get; }
    }
}
