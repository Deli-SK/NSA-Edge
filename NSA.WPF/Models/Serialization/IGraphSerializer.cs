using System.IO;

using NSA.WPF.Models.Business;
using NSA.WPF.Models.Data;

namespace NSA.WPF.Models.Serialization
{
    public interface IGraphSerializer
    {
        bool Serialize(Stream stream, IGraphModel model);
        bool Deserialize(Stream stream, out TermNode[] terms, out SentenceNode[] sentences, out Connection[] connections);
    }
}
