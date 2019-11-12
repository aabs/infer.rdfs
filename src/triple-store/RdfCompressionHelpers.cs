using System;

namespace Inference.Storage
{
    /// <summary>
    /// Helper functions to assist with compression related activities
    /// </summary>
    internal static class RdfCompressionHelpers
    {
        public static int HashTriple(int s, int p, int o) => HashCode.Combine(s, p, o);
    }
}