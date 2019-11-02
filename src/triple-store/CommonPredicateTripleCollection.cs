using System;
using System.Collections;
using System.Collections.Generic;

namespace Inference.Storage
{
    public class CommonPredicateTripleCollection : SuperStore, ITripleStore, IEnumerable<Triple>
    {
        public CommonPredicateTripleCollection(int predicate)
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

        public override Triple ElementAt(int x)
        {
            return new Triple(Doubles[x, 0], predicateId, Doubles[x, 1]);
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
    }
}