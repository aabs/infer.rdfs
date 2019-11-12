using static Inference.Storage.RdfCompressionHelpers;
namespace Inference.Storage
{
    /// <summary>
    /// A singleton class to handle the shared state involved in compressing RDF
    /// </summary>
    public sealed class RdfCompressionContext
    {
        private static readonly RdfCompressionContext _instance = new RdfCompressionContext();
        public UriRegistry UriRegistry { get; } = new UriRegistry();

        private RdfCompressionContext()
        {
        }

        public static RdfCompressionContext Instance
        {
            get { return _instance; }
        }
    }
}