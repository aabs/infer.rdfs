using System;
using System.Collections.Generic;

namespace Inference.Storage
{
    public static class TripleExtensions
    {
        public static IEnumerable<Triple> Match___O<TStore>(this TStore store, Uri o)
        where TStore : IRandomAccessStore
        {
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(o);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.ObjectOrdinalAt(i) == oTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match__P_<TStore>(this TStore store, Uri p)
        where TStore : IRandomAccessStore
        {
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(p);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.PredicateOrdinalAt(i) == oTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match_S__<TStore>(this TStore store, Uri s)
        where TStore : IRandomAccessStore
        {
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(s);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.SubjectOrdinalAt(i) == oTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match_SP_<TStore>(this TStore store, Uri s, Uri p)
        where TStore : IRandomAccessStore
        {
            var sTarget = RdfCompressionContext.Instance.UriRegistry.Get(s);
            var pTarget = RdfCompressionContext.Instance.UriRegistry.Get(p);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.SubjectOrdinalAt(i) == sTarget && store.PredicateOrdinalAt(i) == pTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match_S_O<TStore>(this TStore store, Uri s, Uri o)
        where TStore : IRandomAccessStore
        {
            var sTarget = RdfCompressionContext.Instance.UriRegistry.Get(s);
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(o);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.SubjectOrdinalAt(i) == sTarget && store.ObjectOrdinalAt(i) == oTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match__PO<TStore>(this TStore store, Uri p, Uri o)
        where TStore : IRandomAccessStore
        {
            var pTarget = RdfCompressionContext.Instance.UriRegistry.Get(p);
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(o);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.ObjectOrdinalAt(i) == oTarget && store.PredicateOrdinalAt(i) == pTarget)
                    yield return store.ElementAt(i);
            }
        }

        public static IEnumerable<Triple> Match_SPO<TStore>(this TStore store, Uri s, Uri p, Uri o)
        where TStore : IRandomAccessStore
        {
            var sTarget = RdfCompressionContext.Instance.UriRegistry.Get(s);
            var pTarget = RdfCompressionContext.Instance.UriRegistry.Get(p);
            var oTarget = RdfCompressionContext.Instance.UriRegistry.Get(o);
            for (int i = 0; i < store.Count; i++)
            {
                if (store.SubjectOrdinalAt(i) == sTarget && store.PredicateOrdinalAt(i) == pTarget && store.ObjectOrdinalAt(i) == oTarget)
                    yield return store.ElementAt(i);
            }
        }
    }
}