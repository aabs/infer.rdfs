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
    [TestFixture]
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
        [Test]
        public void TestCanMaterialiseExtendedSubclasses()
        {
            // the premise is that: if (a rdfs:subClassOf b && b rdfs:subClassOf c && c rdfs:subClassOf d) then ( a rdfs:subClassOf d )
            // and that transitively, this should apply indefinitely.

            _assert("a:A", "rdfs:subClassOf", "a:B");
            _assert("a:B", "rdfs:subClassOf", "a:C");
            _assert("a:C", "rdfs:subClassOf", "a:D");
            _assert("a:D", "rdfs:subClassOf", "a:E");
            Assert.False(is_asserted("a:A", "rdfs:subClassOf", "a:E"));
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            Assert.True(is_asserted("a:A", "rdfs:subClassOf", "a:E"));
        }
        [Test]
        public void TestCanMaterialiseExtendedSubproperties()
        {
            // the premise is that: if (a rdfs:subClassOf b && b rdfs:subClassOf c && c rdfs:subClassOf d) then ( a rdfs:subClassOf d )
            // and that transitively, this should apply indefinitely.

            _assert("a:A", "rdfs:subPropertyOf", "a:B");
            _assert("a:B", "rdfs:subPropertyOf", "a:C");
            _assert("a:C", "rdfs:subPropertyOf", "a:D");
            _assert("a:D", "rdfs:subPropertyOf", "a:E");
            _assert("a:x", "a:A", "a:y");
            Assert.False(is_asserted("a:x", "a:E", "a:y"));
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            Assert.True(is_asserted("a:x", "a:E", "a:y"));
        }

        [Test]
        public void TestSubpropertyPropogation()
        {
            _assert("a:isWifeOf", "rdfs:domain", "a:Woman");
            _assert("a:isWifeOf", "rdfs:subPropertyOf", "a:isSpouseOf");
            _assert("a:isSpouseOf", "rdfs:range", "a:Person");
            _assert("a:Anne", "a:isWifeOf", "a:Charlie");
            Assert.False(is_asserted("a:Charlie", "rdf:type", "a:Person"));
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            Assert.True(is_asserted("a:Charlie", "rdf:type", "a:Person"));
        }
    }
}
