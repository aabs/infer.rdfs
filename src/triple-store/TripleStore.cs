using System;
using System.Collections.Generic;

namespace triple_store
{
    public class TripleStore
    {
        public UriRegistry UriRegistry { get; set; }
        public Dictionary<int, int> _tripleMap = new Dictionary<int, int>();
        // 3D sparse matrix for adjacency
        public const int ArraySizeIncrement = 1024;

        private int nextIndex = 0;
        private object _lock = new object();
        public int[,] Triples = new int[ArraySizeIncrement, 3];

        public int HashTriple(int s, int p, int o)
            => HashCode.Combine(s, p, o);

        public int InsertTriple(Triple t)
        {
            lock (_lock)
            {
                if (!_tripleMap.ContainsKey(t.GetHashCode()))
                {
                    int destinationIndex;
                    if ((3 * nextIndex) >= Triples.Length)
                {
                    Resize(ref Triples, Triples.Length + ArraySizeIncrement);
                }
                    destinationIndex = nextIndex++;
                    var tt = t.Get();

                    Triples[destinationIndex, 0] = tt.Item1;
                    Triples[destinationIndex, 1] = tt.Item2;
                    Triples[destinationIndex, 2] = tt.Item3;
                    _tripleMap[t.GetHashCode()] = destinationIndex;
                    return destinationIndex;
                }
                return _tripleMap[t.GetHashCode()];
            }
        }

        public static void Resize(ref int[,] array, int newSize)
        {
            var tmp = new int[newSize, 3];
            Array.Copy(array, tmp, array.Length);
            array = tmp;
        }

        public Triple NewTriple(Uri subject, Uri predicate, Uri @object)
            => new Triple(subject, predicate, @object, this.UriRegistry);

        public Triple ElementAt(int x)
        {
            return new Triple(Triples[x, 0], Triples[x, 1], Triples[x, 2], UriRegistry);
        }
    }
}