using System;
using System.Linq;
using Autofac;
using infer_core;
using NUnit.Framework;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Update;

namespace infer_tests
{
    public class InferenceTests : RdfTestBase
    {
        [Test]
        public void TestCanCreateInferenceEngine()
        {
            _assert("a:subj", "owl:sameAs", "a:obj");
            _assertd("a:subj", "a:p1", "hello");
            _assertd("a:subj", "a:p2", "world");
            Assert.That(_tripleStore.Triples.Count(), Is.EqualTo(3));
            var inf = _container.Resolve<IInferenceEngine>();
            Assert.That(inf, Is.Not.Null);
        }
    }
}
