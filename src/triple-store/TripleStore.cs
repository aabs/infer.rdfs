using System;

namespace triple_store
{
    public class TripleStore
    {
        public UriRegistry UriRegistry { get; set; }
        // 3D sparse matrix for adjacency
        public const int ArraySizeIncrement = 1024;
        private int nextIndex = 0;
        private object _lock = new object();
        public int[,] Triples = new int[ArraySizeIncrement, 3];
        public int InsertTriple(Triple t)
        {
            int destinationIndex;
            lock (_lock)
            {
                if((3*nextIndex) >= Triples.Length)
                {
                    Resize(ref Triples, Triples.Length + ArraySizeIncrement);
                }
                destinationIndex = nextIndex++;
                var tt = t.Get();
                Triples[destinationIndex, 0] = tt.Item1;
                Triples[destinationIndex, 1] = tt.Item2;
                Triples[destinationIndex, 2] = tt.Item3;
            }
            return destinationIndex;
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
