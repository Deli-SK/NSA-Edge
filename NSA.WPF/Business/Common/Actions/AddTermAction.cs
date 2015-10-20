using System.Runtime.Serialization;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.Business.Common.Actions
{
    [DataContract]
    public class AddTermAction : GraphAction
    {
        [DataMember]
        public string Term { get; set; }

        public AddTermAction(string term)
        {
            this.Term = term;
        }

        public override bool Do(IGraphFacade facade)
        {
            return facade.AddTerm(this.Term);
        }

        public override bool Undo(IGraphFacade facade)
        {
            return facade.RemoveTerm(this.Term);
        }
    }
}