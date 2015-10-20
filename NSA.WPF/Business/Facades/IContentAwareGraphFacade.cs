using System.IO;

namespace NSA.WPF.Business.Facades
{
    public interface IContentAwareGraphFacade: IGraphFacade
    {
        void Save(Stream stream);
        bool Load(Stream stream);

        bool CanUndo();
        void Undo();

        bool CanRedo();
        void Redo();
    }
}
