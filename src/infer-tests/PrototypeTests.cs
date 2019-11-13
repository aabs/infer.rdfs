using Inference.Storage;
using NUnit.Framework;
using System;
using static Inference.Storage.UriHelpers;

namespace Inference.Test
{
    [TestFixture]
    internal class PrototypeTests
    {
        [Test]
        public void TestJoin1()
        {
        }

        public TripleCollection CreateSeededStore()
        {
            var ts = new TripleCollection();
            ts
                .Assert(u("urn:john"), u("urn:spouse"), u("urn:janet"))
                .Assert(u("urn:john"), u("urn:brother"), u("urn:fred"))
                .Assert(u("urn:brother"), u("run:rdf:type"), u("urn:Reflexive"))
                ;

            throw new NotImplementedException();
        }
    }


}