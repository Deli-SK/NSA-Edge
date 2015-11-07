using System.Collections.ObjectModel;
using System.IO;
using NSA.WPF.Models.Data;

namespace NSA.WPF.Models.Business
{
    public interface IGraphModel
    {
        ReadOnlyObservableCollection<TermNode> Terms { get; }
        ReadOnlyObservableCollection<SentenceNode> Sentences { get; }
        ReadOnlyObservableCollection<Connection> Connections { get; }
        
        SentenceNode AddSentence(uint chapter, uint sentence);
        TermNode AddTerm(string term);
        Connection AddConnection(Node from, Node to);

        void Clear();
        bool SaveTo(Stream stream);
        bool LoadFrom(Stream stream);

        bool RemoveSentence(SentenceNode node);
        bool RemoveTerm(TermNode node);
        bool RemoveConnection(Connection connection);
    }
}