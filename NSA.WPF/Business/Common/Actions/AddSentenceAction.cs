using System.Runtime.Serialization;
using NSA.WPF.Business.Facades;

namespace NSA.WPF.Business.Common.Actions
{
    [DataContract]
    public class AddSentenceAction : GraphAction
    {
        [DataMember]
        public uint Page { get; set; }

        [DataMember]
        public uint Sentence { get; set; }

        public AddSentenceAction(uint page, uint sentence)
        {
            this.Page = page;
            this.Sentence = sentence;
        }

        public override bool Do(IGraphFacade facade)
        {
            return facade.AddSentence(this.Page, this.Sentence);
        }

        public override bool Undo(IGraphFacade facade)
        {
            return facade.RemoveSentence(this.Page, this.Sentence);
        }
    }
}