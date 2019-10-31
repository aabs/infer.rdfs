using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace triple_store
{
    public class Triple : IEquatable<Triple>
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

        public Triple(int s, int p, int o, UriRegistry externalRegistry = null)
        {
            if (externalRegistry != null)
            {
                effectiveIndex = externalRegistry;
            }
            else
            {
                effectiveIndex = defaultIndex;
            }
            _subject = s;
            _predicate = p;
            _object = o;
        }
        public (int, int, int) Get()
            => (_subject, _predicate, _object);

        public override bool Equals(object obj)
        {
            return Equals(obj as Triple);
        }

        public bool Equals([AllowNull] Triple other)
        {
            return other != null &&
                   _subject == other._subject &&
                   _predicate == other._predicate &&
                   _object == other._object;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_subject, _predicate, _object);
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

        public static bool operator ==(Triple left, Triple right)
        {
            return EqualityComparer<Triple>.Default.Equals(left, right);
        }

        public static bool operator !=(Triple left, Triple right)
        {
            return !(left == right);
        }
    }
}