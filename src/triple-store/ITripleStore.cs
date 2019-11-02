namespace Inference.Storage
{
    public interface ITripleStore
    {
        int Count { get; }

        Triple ElementAt(int x);
        int InsertTriple(Triple t);
    }
}