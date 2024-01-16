using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Inference.Storage
{
    public class TripleCollection : SuperStore, ITripleStore, IEnumerable<Triple>, IRandomAccessStore
    {
        // 3D sparse matrix for adjacency

        private int nextIndex = 0;
        private int[,] triples = new int[ArraySizeIncrement, 3];

        public override int InsertTriple(Triple t)
        {
            lock (_lock)
            {
                if (!_tripleMap.ContainsKey(t.GetHashCode()))
                {
                    int destinationIndex;
                    if (3 * nextIndex >= triples.Length)
                    {
                        Resize(ref triples, triples.Length + ArraySizeIncrement);
                    }
                    destinationIndex = nextIndex++;
                    var (s,p,o) = t.Get();

                    triples[destinationIndex, 0] = s;
                    triples[destinationIndex, 1] = p;
                    triples[destinationIndex, 2] = o;
                    _tripleMap[t.GetHashCode()] = destinationIndex;
                    return destinationIndex;
                }
                return _tripleMap[t.GetHashCode()];
            }
        }

        public override int Count { get => nextIndex; }

        public static void Resize(ref int[,] array, int newSize)
        {
            var tmp = new int[newSize, 3];
            Array.Copy(array, tmp, array.Length);
            array = tmp;
        }

        #region Enumeration And Access

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
        #endregion

        #region IRandomAccessStore
        public Triple ElementAt(int index)
        {
            return new Triple(triples[index, 0], triples[index, 1], triples[index, 2]);
        }

        public (int, int, int) OrdinalsAt(int index)
        {
            return (triples[index, 0], triples[index, 1], triples[index, 2]);
        }

        public Uri SubjectAt(int index) 
            => RdfCompressionContext.Instance.UriRegistry.Lookup(SubjectOrdinalAt(index));

        public Uri PredicateAt(int index)
            => RdfCompressionContext.Instance.UriRegistry.Lookup(PredicateOrdinalAt(index));

        public Uri ObjectAt(int index)
            => RdfCompressionContext.Instance.UriRegistry.Lookup(ObjectOrdinalAt(index));

        public int SubjectOrdinalAt(int index) => triples[index, 0];

        public int PredicateOrdinalAt(int index) => triples[index, 1];

        public int ObjectOrdinalAt(int index) => triples[index, 2];

        #endregion Enumeration And Access

        #region Enumeration
        [SuppressMessage("Performance", "CA1815")]
        [SuppressMessage("Usage", "CA2231")]
        public struct Enumerator
        {
            private IRandomAccessStore _store;
            private int _currentIndex;

            public Triple Current => _store.ElementAt(_currentIndex);

            internal Enumerator(IRandomAccessStore store)
            {
                _store = store;
                _currentIndex = 0;
            }

            public bool MoveNext()
            {
                if (_currentIndex < _store.Count)
                {
                    _currentIndex++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _currentIndex = 0;
            }

            public override bool Equals(object obj) => throw new NotSupportedException();

            public override int GetHashCode() => throw new NotSupportedException();
        }

        public class EnumeratorImpl : IEnumerator<Triple>
        {
            private Enumerator _en;

            internal EnumeratorImpl(IRandomAccessStore store)
            {
                _en = new Enumerator(store);
            }

            public Triple Current => _en.Current;

            object IEnumerator.Current => _en.Current;

            Triple IEnumerator<Triple>.Current => _en.Current;

            public bool MoveNext() => _en.MoveNext();

            public void Reset() => _en.Reset();

            public void Dispose()
            {
            }

            bool IEnumerator.MoveNext() => _en.MoveNext();

            void IEnumerator.Reset() => _en.Reset();

            void IDisposable.Dispose()
            {
            }
        }


        #endregion
    }
}