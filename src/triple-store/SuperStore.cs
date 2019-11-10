using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Inference.Storage
{
    public abstract class SuperStore : ITripleStore
    {
        public const int ArraySizeIncrement = 1024;
        protected object _lock = new object();
        public Dictionary<int, int> _tripleMap = new Dictionary<int, int>();

        public abstract int Count { get; }

        [SuppressMessage("Performance", "CA1815")]
        [SuppressMessage("Usage", "CA2231")]
        public struct Enumerator
        {
            private ITripleStore _store;
            private int _currentIndex;

            internal Enumerator(ITripleStore store)
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

            public Triple Current
            {
                get => _store.ElementAt(_currentIndex);
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

            internal EnumeratorImpl(ITripleStore store)
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

        public abstract Triple ElementAt(int x);

        public abstract int InsertTriple(Triple t);

        public Triple NewTriple(Uri subject, Uri predicate, Uri @object)
            => new Triple(subject, predicate, @object);
    }
}