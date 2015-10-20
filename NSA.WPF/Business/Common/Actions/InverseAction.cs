using System.Runtime.Serialization;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.Business.Common.Actions
{
    [DataContract]
    public class InverseAction: GraphAction
    {
        [DataMember]
        public GraphAction Action { get; set; }

        public InverseAction(GraphAction action)
        {
            this.Action = action;
        }

        public override bool Do(IGraphFacade facade)
        {
            return this.Action.Undo(facade);
        }

        public override bool Undo(IGraphFacade facade)
        {
            return this.Action.Do(facade);
        }
    }
}
