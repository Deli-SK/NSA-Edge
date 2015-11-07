using System.Runtime.Serialization;

namespace NSA.WPF.Models.Data
{
    [DataContract]
    public class TermNode: Node
    {
        [DataMember]
        public string Term { get; }

        public override string Label => this.Term;

        public TermNode(string term)
        {
            this.Term = term;
        }
    }
}
