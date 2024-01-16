using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Inference.Storage
{
    /// <summary>
    /// A Triple store that is composed of various PropertyStores
    /// </summary>
    public class ComposedTripleStore : ITripleStore, IEnumerable<Triple>
    {
        private Dictionary<int, PropertyStore> stores = new Dictionary<int, PropertyStore>();

        public int Count { get; }
        public Dictionary<int, PropertyStore> Stores { get => stores; set => stores = value; }

        public Triple ElementAt(int x)
        {
            var ctr = x;
            var storesCtr = Stores.Count;
            for (int s = 0; s < storesCtr; s++)
            {
                IEnumerable<KeyValuePair<int, PropertyStore>> stores = Stores.AsEnumerable();
                KeyValuePair<int, PropertyStore> currentStore = stores.ElementAt(s);
                int sizeOfCurrentTable = currentStore.Value.Count;
                if (sizeOfCurrentTable < ctr)
                {
                    ctr -= sizeOfCurrentTable;
                    continue;
                }
                else
                {
                    return currentStore.Value.ElementAt(ctr);
                }
            }

            return default(Triple);
        }

        public int InsertTriple(Triple t)
        {
            (int s, int p, int o) = t.Get();
            if (!Stores.ContainsKey(p))
            {
                Stores.Add(p, new PropertyStore(p));
            }
            Stores[p].InsertTriple(t);
            return 0;
        }

        IEnumerator<Triple> IEnumerable<Triple>.GetEnumerator()
        {
            return new EnumeratorImpl(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EnumeratorImpl(this);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        [SuppressMessage("Performance", "CA1815")]
        [SuppressMessage("Usage", "CA2231")]
        public struct Enumerator
        {
            private IEnumerator<Triple> _store;
            private Triple _current;
            private readonly ComposedTripleStore ctStore;

            internal Enumerator(ComposedTripleStore store)
            {
                _store = store.Stores.SelectMany(x => x.Value).GetEnumerator();
                _current = null;
                ctStore = store;
            }

            public bool MoveNext() => _store.MoveNext();

            public Triple Current => _store.Current;

            public void Reset() => _store.Reset();

            public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

            public override int GetHashCode() => Enumerable.Aggregate(ctStore.Stores.SelectMany(x => x.Value), 0, (a, x) => a ^ x.GetHashCode());
        }

        private class EnumeratorImpl : IEnumerator<Triple>
        {
            private Enumerator _en;

            internal EnumeratorImpl(ComposedTripleStore store)
            {
                _en = new Enumerator(store);
            }

            public Triple Current => _en.Current;

            object IEnumerator.Current => _en.Current;

            public bool MoveNext() => _en.MoveNext();

            public void Reset() => _en.Reset();

            public void Dispose()
            {
            }
        }
    }
}