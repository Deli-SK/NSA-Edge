using System;
using System.Collections.Generic;
using System.IO;
using System.Security.RightsManagement;
using NSA.WPF.Business.Common.Actions;
using NSA.WPF.Business.Common;

namespace NSA.WPF.Business.Facades
{
    public interface IGraphFacade
    {
        event Action Change;

        bool AddSentence(uint page, uint sentence);
        bool AddTerm(string term);
        bool Connect(uint page, uint sentence, string term, ConnectionType type);
        bool RemoveSentence(uint page, uint sentence);
        bool RemoveTerm(string term);
        bool Disconnect(uint page, uint sentence, string term);

        bool ContainsTerm(string term);
        bool ContainsSentence(uint page, uint sentence);
        bool HasConnection(uint page, uint sentence, string term);

        IEnumerable<string> GetTerms();
        IEnumerable<uint> GetPages();
        IEnumerable<uint> GetSentences(uint page);
        void Clear();
    }
}