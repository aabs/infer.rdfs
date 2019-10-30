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
        public Triple[] Triples = new Triple[ArraySizeIncrement];
        public int InsertTriple(Triple t)
        {
            int destinationIndex;
            lock (_lock)
            {
                if(nextIndex == Triples.Length)
                {
                    Array.Resize<Triple>(ref Triples, Triples.Length + ArraySizeIncrement);
                }
                destinationIndex = nextIndex++;
            }
            Triples[destinationIndex] = t;
            return destinationIndex;
        }

        public Triple NewTriple(Uri subject, Uri predicate, Uri @object) 
            => new Triple(subject, predicate, @object, this.UriRegistry);

        public Triple ElementAt(int x)
        {
            return Triples[x];
        }
    }
}
