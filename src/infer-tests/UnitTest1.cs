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
    public class InferenceTests
    {
        #region helpers

        private IContainer _container;
        private TripleStore _tripleStore;
        private Graph _defaultGraph;
        private Graph _inferences;
        void seedGraph()
        {
            _defaultGraph.NamespaceMap.AddNamespace("a", new Uri("http://tempuri.com/blah/"));
            _defaultGraph.NamespaceMap.AddNamespace("owl", new Uri("http://www.w3.org/2002/07/owl#"));
            _assert("a:subj", "owl:sameAs", "a:obj");
            _assertd("a:subj", "a:p1", "hello");
            _assertd("a:subj", "a:p2", "world");
        }
        IUriNode qname(string name) => _defaultGraph.CreateUriNode(name);
        IUriNode node(string name) => _defaultGraph.CreateUriNode($"a:{name}");
        ILiteralNode literal(string value) => _defaultGraph.CreateLiteralNode(value);
        void _assert(string s, string p, string o)
        {
            _defaultGraph.Assert(qname(s), qname(p), qname(o));
        }
        void _assertd<T>(string s, string p, T l)
        {
            _defaultGraph.Assert(qname(s), qname(p), literal(l.ToString()));
        }
        bool has_inferred_triple(string s, string p, string o)
        {
            var results1 = _inferences.GetTriplesWithSubjectPredicate(qname(s), qname(p));
            var nobj = qname(o);
            return results1.Any(t => t.Object.Equals(nobj));
        }

        [SetUp]
        public void Setup()
        {
            _tripleStore = new TripleStore();
            _defaultGraph = new Graph();
            _inferences = new Graph();
            _inferences.BaseUri = new Uri("http://industrialinference.com/inferred/");
            _tripleStore.Add(_defaultGraph);
            _tripleStore.Add(_inferences);
            var qp = new LeviathanQueryProcessor(_tripleStore);
            var up = new LeviathanUpdateProcessor(_tripleStore);
            var builder = new ContainerBuilder();
            builder.RegisterType<InferenceEngine>().AsImplementedInterfaces();
            builder.Register<ISparqlQueryProcessor>(_ => qp);
            builder.Register<ISparqlUpdateProcessor>(_ => up);
            _container = builder.Build();
        }
        #endregion

        [Test]
        public void TestCanCreateInferenceEngine()
        {
            seedGraph();
            Assert.That(_tripleStore.Triples.Count(), Is.EqualTo(3));
            var inf = _container.Resolve<IInferenceEngine>();
            Assert.That(inf, Is.Not.Null);
        }

        [Test]
        public void TestCanRunInference()
        {
            seedGraph();
            Assert.That(_tripleStore.Triples.Count(), Is.EqualTo(3));
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            Assert.That(_tripleStore.Triples.Count(), Is.Not.EqualTo(3));
        }

        [Test]
        public void TestSameAsPropogatesProperties()
        {
            seedGraph();
            var b = qname("a:obj");
            var p1 = qname("a:p1");
            var p2 = qname("a:p2");
            var results1 = _inferences.GetTriplesWithSubjectPredicate(b, p1);
            var results2 = _inferences.GetTriplesWithSubjectPredicate(b, p1);
            Assert.That(results1, Is.Empty);
            Assert.That(results2, Is.Empty);
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            results1 = _inferences.GetTriplesWithSubjectPredicate(b, p1);
            results2 = _inferences.GetTriplesWithSubjectPredicate(b, p1);
            Assert.That(results1, Is.Not.Empty);
            Assert.That(results2, Is.Not.Empty);
        }

        [Test]
        public void Test_rdfs2()
        {
            seedGraph();
            _assert("a:p1", "rdfs:domain", "a:SomeClass");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            Assert.That(has_inferred_triple("a:subj", "rdf:type", "a:SomeClass"), Is.True);
        }
        [Test]
        public void Test_rdfs3()
        {
            seedGraph();
            _assert("a:subj", "a:p3", "a:obj2");
            _assert("a:p3", "rdfs:range", "a:SomeOtherClass");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer();
            Assert.That(has_inferred_triple("a:obj2", "rdf:type", "a:SomeOtherClass"), Is.True);
        }
    }
}
