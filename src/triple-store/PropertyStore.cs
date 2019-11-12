using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Inference.Storage
{
    /// <summary>
    /// A Triple store where all the triples share a common predicate.
    /// </summary>
    /// <remarks>This store provides a way to compress slightly</remarks>
    public class PropertyStore : SuperStore, ITripleStore, IEnumerable<Triple>, IRandomAccessStore
    {
        public PropertyStore(int predicate)
        {
            predicateId = predicate;
        }

        public int predicateId { get; }
        private int nextIndex = 0;
        public int[,] Doubles = new int[ArraySizeIncrement, 2];
        public override int Count { get => nextIndex; }

        public override int InsertTriple(Triple t)
        {
            (int s, int p, int o) tt = t.Get();
            if (tt.p != predicateId)
            {
                throw new ArgumentException("unable to add triple to mismatched predicateStore");
            }
            int tripleHash = t.GetHashCode();
            lock (_lock)
            {
                if (!_tripleMap.ContainsKey(tripleHash))
                {
                    int destinationIndex;
                    if (3 * nextIndex >= Doubles.Length)
                    {
                        Resize(ref Doubles, Doubles.Length + ArraySizeIncrement);
                    }
                    destinationIndex = nextIndex++;

                    Doubles[destinationIndex, 0] = tt.s;
                    Doubles[destinationIndex, 1] = tt.o;
                    _tripleMap[tripleHash] = destinationIndex;
                    return destinationIndex;
                }
                return _tripleMap[tripleHash];
            }
        }

        public static void Resize(ref int[,] array, int newSize)
        {
            var tmp = new int[newSize, 2];
            Array.Copy(array, tmp, array.Length);
            array = tmp;
        }

        IEnumerator<Triple> IEnumerable<Triple>.GetEnumerator()
        {
            return new EnumeratorImpl(this);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EnumeratorImpl(this);
        }

        #region IRandomAccessStore

        public Triple ElementAt(int index)
        {
            return new Triple(Doubles[index, 0], predicateId, Doubles[index, 1]);
        }

        public (int, int, int) OrdinalsAt(int index)
        {
            return (Doubles[index, 0], predicateId, Doubles[index, 1]);
        }

        public Uri SubjectAt(int index)
            => RdfCompressionContext.Instance.UriRegistry.Lookup(SubjectOrdinalAt(index));

        public Uri PredicateAt(int index)
            => RdfCompressionContext.Instance.UriRegistry.Lookup(PredicateOrdinalAt(index));

        public Uri ObjectAt(int index)
            => RdfCompressionContext.Instance.UriRegistry.Lookup(ObjectOrdinalAt(index));

        public int SubjectOrdinalAt(int index) => Doubles[index, 0];

        public int PredicateOrdinalAt(int index) => predicateId;

        public int ObjectOrdinalAt(int index) => Doubles[index, 1];

        #endregion IRandomAccessStore

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

        #endregion Enumeration
    }
}