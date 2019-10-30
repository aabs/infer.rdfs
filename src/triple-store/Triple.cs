using System;

namespace triple_store
{
    public class Triple
    {
        private static UriRegistry defaultIndex = new UriRegistry();
        private UriRegistry effectiveIndex = new UriRegistry();
        public Triple(Uri subject, Uri predicate, Uri @object, UriRegistry externalRegistry = null) 
        {
            if (externalRegistry != null)
            {
                effectiveIndex = externalRegistry;
            } 
            else
            {
                effectiveIndex = defaultIndex;
            }

            Subject = subject;
            Predicate = predicate;
            Object = @object;
        }

        int _subject;
        public Uri Subject
        {
            get
            {
                return effectiveIndex.Lookup(_subject);
            }
            set
            {
                _subject = effectiveIndex.Add(value);
            }
        }

        int _predicate;
        public Uri Predicate
        {
            get
            {
                return effectiveIndex.Lookup(_predicate);
            }
            set
            {
                _predicate = effectiveIndex.Add(value);
            }
        }

        int _object;
        public Uri Object
        {
            get
            {
                return effectiveIndex.Lookup(_object);
            }
            set
            {
                _object = effectiveIndex.Add(value);
            }
        }
    }
}