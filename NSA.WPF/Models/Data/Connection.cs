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

        private bool Equals(Connection other)
        {
            return Equals(this.From, other.From) && Equals(this.To, other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((Connection) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.From?.GetHashCode() ?? 0) * 397) ^ (this.To?.GetHashCode() ?? 0);
            }
        }
    }
}
