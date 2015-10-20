namespace NSA.WPF.Business.Data.Nodes
{
    public class TermNode: INode
    {
        public string Term { get; }

        public TermNode(string term)
        {
            this.Term = term;
        }

        public static string GetLabel(string term)
        {
            return term;
        }

        public override string ToString()
        {
            return TermNode.GetLabel(this.Term);
        }

        private bool Equals(TermNode other)
        {
            return string.Equals(this.Term, other.Term);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((TermNode) obj);
        }

        public override int GetHashCode()
        {
            return this.Term?.GetHashCode() ?? 0;
        }
        
        
    }
}
