using System.Collections.Generic;
using NSA.WPF.Common.Cacheing;
using NSA.WPF.Models.Data;

namespace NSA.WPF.Services
{
    public interface IGraphLayoutingService
    {
        void AttachNodeSource(IValueProvider<ICollection<Node>> nodes);
        void AttachConnectionSource(IValueProvider<ICollection<Connection>> connections);
    }
}
