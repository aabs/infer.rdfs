using System;

namespace Inference.Storage
{
    public interface IRandomAccessStore
    {
        int Count { get; }
        Triple ElementAt(int index);
        (int, int, int) OrdinalsAt(int index);
        Uri SubjectAt(int index);
        Uri PredicateAt(int index);
        Uri ObjectAt(int index);
        int SubjectOrdinalAt(int index);
        int PredicateOrdinalAt(int index);
        int ObjectOrdinalAt(int index);
    }
}