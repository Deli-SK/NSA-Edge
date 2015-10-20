using System.Collections.Generic;
using NSA.WPF.Business.Common;

namespace NSA.WPF.Business.Data.Nodes
{
    public class SentenceNode: INode
    {
        public uint Page { get; }

        public uint Sentence { get; }

        private Dictionary<TermNode, ConnectionType> _connectedTerms;
        
        public SentenceNode(uint page, uint sentence)
        {
            this.Page = page;
            this.Sentence = sentence;
        }

        public static string GetLabel(uint page, uint sentence)
        {
            return $"{page},{sentence}";
        }

        public void AddConnection(TermNode termNode, ConnectionType type)
        {
            if (this._connectedTerms == null)
                this._connectedTerms = new Dictionary<TermNode, ConnectionType>();

            this._connectedTerms.Add(termNode, type);
        }

        public bool HasConnection(TermNode termNode)
        {
            return this._connectedTerms != null
                   && this._connectedTerms.ContainsKey(termNode);
        }

        public bool RemoveConnection(TermNode termNode)
        {
            return this._connectedTerms != null
                   && this._connectedTerms.Remove(termNode);
        }

        public override string ToString()
        {
            return GetLabel(this.Page, this.Sentence);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((SentenceNode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)this.Page * 397) ^ (int)this.Sentence;
            }
        }

        private bool Equals(SentenceNode other)
        {
            return this.Page == other.Page && this.Sentence == other.Sentence;
        }


    }
}
