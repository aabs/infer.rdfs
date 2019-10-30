using System;

namespace triple_store
{
    class Triple : Tuple<int, int, int>
    {
        public Triple(int subj, int pred, int obj) 
            : base(subj, pred, obj)
        {
        }
    }
}