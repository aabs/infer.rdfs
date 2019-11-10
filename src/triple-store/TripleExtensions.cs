using System;
using System.Collections.Generic;

namespace Inference.Storage
{
    public static class TripleExtensions
    {
        public static IEnumerable<Triple> Match___O<TStore>(this TStore store, Uri o)
        where TStore : IRandomAccessStore, ITripleStore
        {
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(o);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.ObjectOrdinalAt(i) == oTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match__P_<TStore>(this TStore store)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Triple> Match_S__<TStore>(this TStore store)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Triple> Match_SP_<TStore>(this TStore store)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Triple> Match_S_O<TStore>(this TStore store)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Triple> Match__PO<TStore>(this TStore store)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Triple> Match_SPO<TStore>(this TStore store)
        {
            throw new NotImplementedException();
        }
    }
}