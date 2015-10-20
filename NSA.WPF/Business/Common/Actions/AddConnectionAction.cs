using System.Runtime.Serialization;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.Business.Common.Actions
{
    [DataContract]
    public class AddConnectionAction : GraphAction
    {
        [DataMember]
        public uint Page { get; set; }

        [DataMember]
        public uint Sentence { get; set; }

        [DataMember]
        public string Term { get; set; }

        [DataMember]
        public ConnectionType ConnectionType { get; set; }

        public AddConnectionAction(uint page, uint sentence, string term, ConnectionType connectionType)
        {
            this.Page = page;
            this.Sentence = sentence;
            this.Term = term;
            this.ConnectionType = connectionType;
        }

        public override bool Do(IGraphFacade facade)
        {
            return facade.Connect(this.Page, this.Sentence, this.Term, this.ConnectionType);
        }

        public override bool Undo(IGraphFacade facade)
        {
            return facade.Disconnect(this.Page, this.Sentence, this.Term);
        }
    }
}