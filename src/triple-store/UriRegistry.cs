using System;
using System.Collections.Generic;
using System.Threading;

namespace triple_store
{
    public class UriRegistry
    {
        // LUT for URIs to int
        private int UriId = 0;
        Dictionary<int, Uri> lutUris = new Dictionary<int, Uri>();
        Dictionary<int, int> rlutUris = new Dictionary<int, int>(); // reverse lookup from URI hashcode to ID

        public int Add(Uri u)
        {
            var hashcode = u.GetHashCode();
            if (rlutUris.ContainsKey(hashcode))
            {
                return rlutUris[hashcode];
            }

            var val = Interlocked.Increment(ref UriId);
            rlutUris[hashcode] = val;
            lutUris[val] = u;
            return val;
        }
        public Uri Lookup(int i)
        {
            return lutUris[i];
        }
        public int Get(Uri u)
        {
            var hashCode = u.GetHashCode();
            if (rlutUris.ContainsKey(hashCode))
            {
                return rlutUris[hashCode];
            }
            throw new ApplicationException("URI not recognised");
        }

    }
}