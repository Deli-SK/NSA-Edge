namespace NSA.Data.Graph.Elements
{
    public class Connection: CompoundElement
    {
        public Node SourceNode { get; }
        public Node TargetNode { get; }

        public Connection(Node sourceNode, Node targetNode)
        {
            this.SourceNode = sourceNode;
            this.TargetNode = targetNode;
        }
    }
}
