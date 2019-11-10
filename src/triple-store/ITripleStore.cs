using System;
using System.Collections.Generic;
using System.Linq;

namespace Inference.Storage
{
    public interface ITripleStore
    {
        int InsertTriple(Triple t);

    }

    public interface ITripleMatch
    {
        IEnumerable<Triple> Match___O(Uri o);
        IEnumerable<Triple> Match__P_();
        IEnumerable<Triple> Match_S__();
        IEnumerable<Triple> Match_SP_();
        IEnumerable<Triple> Match_S_O();
        IEnumerable<Triple> Match__PO();
        IEnumerable<Triple> Match_SPO();
    }
}