using System.Runtime.Serialization;

namespace NSA.WPF.Models.Data
{
    [DataContract]
    public class Connection
    {
        [DataMember]
        public Node From { get; }

        [DataMember]
        public Node To { get; }

        public Connection(Node from, Node to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
