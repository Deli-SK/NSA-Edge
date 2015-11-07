using System;

namespace NSA.WPF.Services
{
    public interface IStateService
    {
        event EventHandler UndoCalled;
        event EventHandler RedoCalled;
        event EventHandler ActionRegistered;
        
        bool CanUndo();
        bool CanRedo();

        void Undo();
        void Redo();

        void RegisterAction(Action undo, Action redo);
    }
}
