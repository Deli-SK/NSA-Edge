using System.Runtime.Serialization;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.Business.Common.Actions
{
    [DataContract]
    [KnownType(typeof(AddTermAction))]
    [KnownType(typeof(AddSentenceAction))]
    [KnownType(typeof(AddConnectionAction))]
    [KnownType(typeof(InverseAction))]
    public abstract class GraphAction
    {
        public abstract bool Do(IGraphFacade facade);
        public abstract bool Undo(IGraphFacade facade);
    }
}
