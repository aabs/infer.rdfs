using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
            throw new System.NotImplementedException();
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
            private IEnumerable<Triple> _store;
            private Triple _current;

            internal Enumerator(ComposedTripleStore store)
            {

                _store = store;
                _current = null;
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public Triple Current
            {
                get { throw new NotImplementedException(); }
            }

            public void Reset()
            {
                _current = null;
            }

            public override bool Equals(object obj) => throw new NotSupportedException();

            public override int GetHashCode() => throw new NotSupportedException();
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