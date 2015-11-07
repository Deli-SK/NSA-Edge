using System.Collections.Generic;
using System.Windows.Input;
using NSA.WPF.Annotations;
using NSA.WPF.Models.Data;

namespace NSA.WPF.ViewModels
{
    public interface IConnectionsViewModel
    {
        [UsedImplicitly]
        ISelectSentenceViewModel From { get; }

        [UsedImplicitly]
        ISelectTermViewModel To { get; }

        [UsedImplicitly]
        ICommand AddConnection { get; }

        [UsedImplicitly]
        ICommand RemoveConnection { get; }
    }
}
