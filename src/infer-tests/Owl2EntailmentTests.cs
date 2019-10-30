using System;
using System.Linq;
using Autofac;
using infer_core;
using NUnit.Framework;
using Shouldly;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Update;

namespace infer_tests
{
    public class Owl2EntailmentTests : RdfTestBase
    {

        [Test]
        public void TestCanRunInference()
        {
            _assert("a:subj", "owl:sameAs", "a:obj");
            _assertd("a:subj", "a:p1", "hello");
            _assertd("a:subj", "a:p2", "world");
            Assert.That(_tripleStore.Triples.Count(), Is.EqualTo(3));
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null, EntailmentRegime.RDFSPLUS);
            Assert.That(_tripleStore.Triples.Count(), Is.Not.EqualTo(3));
        }

        [Test]
        public void TestSameAsPropogatesProperties()
        {
            _assert("a:subj", "owl:sameAs", "a:obj");
            _assertd("a:subj", "a:p1", "hello");
            _assertd("a:subj", "a:p2", "world");
            var obj = qname("a:obj");
            var p1 = qname("a:p1");
            var p2 = qname("a:p2");
            has_literal("a:obj", "a:p1", "hello").ShouldBe(false);
            has_literal("a:obj", "a:p2", "world").ShouldBe(false);
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null, EntailmentRegime.RDFSPLUS);
            has_literal("a:obj", "a:p1", "hello").ShouldBe(true);
            has_literal("a:obj", "a:p2", "world").ShouldBe(true);
        }


        [Test]
        public void TestSameAsPropogatesProperties2()
        {
            var ts = GetSoccerTripleStore();

            _assert("a:subj", "owl:sameAs", "a:obj");
            _assertd("a:subj", "a:p1", "hello");
            _assertd("a:subj", "a:p2", "world");
            var obj = qname("a:obj");
            var p1 = qname("a:p1");
            var p2 = qname("a:p2");
            has_literal("a:obj", "a:p1", "hello").ShouldBe(false);
            has_literal("a:obj", "a:p2", "world").ShouldBe(false);
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null, EntailmentRegime.RDFSPLUS);
            has_literal("a:obj", "a:p1", "hello").ShouldBe(true);
            has_literal("a:obj", "a:p2", "world").ShouldBe(true);
        }

    }
}
