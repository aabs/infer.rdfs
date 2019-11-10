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

        public abstract int InsertTriple(Triple t);

        public Triple NewTriple(Uri subject, Uri predicate, Uri @object)
            => new Triple(subject, predicate, @object);
    }
}