using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace NSA.WPF.Services
{
    [Export(typeof(IStateService))]
    public class StateService : IStateService
    {
        private class Record
        {
            public Action Undo { get; }
            public Action Redo { get; }

            public Record(Action undo, Action redo)
            {
                this.Undo = undo;
                this.Redo = redo;
            }
        }

        private readonly Stack<Record> _undoRecords;
        private readonly Stack<Record> _redoRecords;
        
        public StateService()
        {
            this._undoRecords = new Stack<Record>(20);
            this._redoRecords = new Stack<Record>(20);
        }

        public bool CanRedo()
        {
            return this._redoRecords.Any();
        }

        public event EventHandler UndoCalled;
        public event EventHandler RedoCalled;
        public event EventHandler ActionRegistered;

        public bool CanUndo()
        {
            return this._undoRecords.Any();
        }

        public void Redo()
        {
            var record = this._redoRecords.Pop();

            record.Redo();

            this._undoRecords.Push(record);
            this._undoRecords.TrimExcess();

            this.OnRedoCalled();
        }

        public void RegisterAction(Action undo, Action redo)
        {
            this._undoRecords.Push(new Record(undo, redo));
            this._undoRecords.TrimExcess();
            this._redoRecords.Clear();

            this.OnActionRegistered();
        }

        public void Undo()
        {
            var record = this._undoRecords.Pop();

            record.Undo();

            this._redoRecords.Push(record);
            this._redoRecords.TrimExcess();

            this.OnUndoCalled();
        }

        protected virtual void OnActionRegistered()
        {
            this.ActionRegistered?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRedoCalled()
        {
            this.RedoCalled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUndoCalled()
        {
            this.UndoCalled?.Invoke(this, EventArgs.Empty);
        }
    }
}