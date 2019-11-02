using System;
using System.Collections;
using System.Collections.Generic;

namespace Inference.Storage
{
    public class TripleCollection : SuperStore, ITripleStore, IEnumerable<Triple>
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
                    var tt = t.Get();

                    triples[destinationIndex, 0] = tt.Item1;
                    triples[destinationIndex, 1] = tt.Item2;
                    triples[destinationIndex, 2] = tt.Item3;
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

        public override Triple ElementAt(int x)
        {
            return new Triple(triples[x, 0], triples[x, 1], triples[x, 2]);
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
    }
}