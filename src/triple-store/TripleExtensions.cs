using System;
using System.Collections.Generic;

namespace Inference.Storage
{
    public static class TripleExtensions
    {
        public static Func<Triple, Uri, bool> fnMatchS = (Triple t, Uri x) => t.SubjOrd == x.ToOrdinal();
        public static Func<Triple, Uri, bool> fnMatchP = (Triple t, Uri x) => t.PredOrd == x.ToOrdinal();
        public static Func<Triple, Uri, bool> fnMatchO = (Triple t, Uri x) => t.ObjOrd == x.ToOrdinal();
        public static Func<Triple, Uri, Uri, bool> fnMatchSP = (Triple t, Uri s, Uri p) => t.SubjOrd == s.ToOrdinal() && t.PredOrd == p.ToOrdinal();
        public static Func<Triple, Uri, Uri, bool> fnMatchSO = (Triple t, Uri s, Uri o) => t.SubjOrd == s.ToOrdinal() && t.ObjOrd == o.ToOrdinal();
        public static Func<Triple, Uri, Uri, bool> fnMatchPO = (Triple t, Uri p, Uri o) => t.PredOrd == p.ToOrdinal() && t.ObjOrd == o.ToOrdinal();
        public static Func<Triple, Uri, Uri, Uri, bool> fnMatchSPO = (Triple t, Uri s, Uri p, Uri o) => t.SubjOrd == s.ToOrdinal() && t.PredOrd == p.ToOrdinal() && t.ObjOrd == o.ToOrdinal();

        private static IEnumerable<Triple> Where(this IEnumerable<Triple> seq, Uri u, Func<Triple, Uri, bool> fn)
        {
            foreach (var item in seq)
            {
                if (fn(item, u))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<Triple> Where(this IEnumerable<Triple> seq, Uri u1, Uri u2, Func<Triple, Uri, Uri, bool> fn)
        {
            foreach (var item in seq)
            {
                if (fn(item, u1, u2))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<Triple> Where(this IEnumerable<Triple> seq, Uri u1, Uri u2, Uri u3, Func<Triple, Uri, Uri, Uri, bool> fn)
        {
            foreach (var item in seq)
            {
                if (fn(item, u1, u2, u3))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<Triple> Match___O<TStore>(this TStore store, Uri o)
        where TStore : IEnumerable<Triple> => store.Where(o, fnMatchO);

        public static IEnumerable<Triple> Match__P_<TStore>(this TStore store, Uri p)
        where TStore : IEnumerable<Triple> => store.Where(p, fnMatchP);

        public static IEnumerable<Triple> Match_S__<TStore>(this TStore store, Uri s)
        where TStore : IEnumerable<Triple> => store.Where(s, fnMatchS);

        public static IEnumerable<Triple> Match_SP_<TStore>(this TStore store, Uri s, Uri p)
        where TStore : IEnumerable<Triple> => store.Where(s, p, fnMatchSP);

        public static IEnumerable<Triple> Match_S_O<TStore>(this TStore store, Uri s, Uri o)
        where TStore : IEnumerable<Triple> => store.Where(s, o, fnMatchSO);

        public static IEnumerable<Triple> Match__PO<TStore>(this TStore store, Uri p, Uri o)
        where TStore : IEnumerable<Triple> => store.Where(p, o, fnMatchPO);

        public static IEnumerable<Triple> Match_SPO<TStore>(this TStore store, Uri s, Uri p, Uri o)
        where TStore : IEnumerable<Triple> => store.Where(s, p, o, fnMatchSPO);

        public static int ToOrdinal(this Uri u) => RdfCompressionContext.Instance.UriRegistry.Get(u);

        public static ITripleStore Assert(this ITripleStore ts, Uri s, Uri p, Uri o)
        {
            ts.InsertTriple(new Triple(s, p, o));
            return ts;
        }
    }
    public static class UriHelpers { 
        public static Uri u(string s)
        {
            Uri result;
            if (Uri.TryCreate(s, UriKind.Absolute, out result)) return result;
            throw new FormatException("Invalid URI format");
        }

    }
}