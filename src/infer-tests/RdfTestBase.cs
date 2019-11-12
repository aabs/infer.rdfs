using Autofac;
using Inference.Core;
using NUnit.Framework;
using System;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Update;

namespace Inference.Test
{
    public class RdfTestBase
    {
        protected IContainer _container;
        protected Graph _contradictions;
        protected Graph _defaultGraph;
        protected Graph _inferences;
        protected NamespaceMapper _nsMap;
        protected TripleStore _tripleStore;

        private Graph CreateGraph(Uri baseUri)
        {
            var result = new Graph();
            if (baseUri != default)
                result.BaseUri = baseUri;
            result.NamespaceMap.Import(_nsMap);
            return result;
        }

        private void SetupNamespaceMap()
        {
            _nsMap = new NamespaceMapper();
            _nsMap.AddNamespace("a", new Uri("http://tempuri.com/blah/"));
            _nsMap.AddNamespace("owl", new Uri("http://www.w3.org/2002/07/owl#"));
            _nsMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
            _nsMap.AddNamespace("rdfs", new Uri("http://www.w3.org/2000/01/rdf-schema#"));
        }

        protected void _assert(string s, string p, string o)
        {
            _defaultGraph.Assert(qname(s), qname(p), qname(o));
        }

        protected void _assertd<T>(string s, string p, T l)
        {
            _defaultGraph.Assert(qname(s), qname(p), literal(l.ToString()));
        }

        protected bool is_asserted(string s, string p, string o)
        {
            var results1 = _inferences.GetTriplesWithSubjectPredicate(qname(s), qname(p));
            var nobj = qname(o);
            return results1.Any(t => t.Object.Equals(nobj));
        }

        protected bool has_literal(string s, string p, string _literal)
        {
            var results1 = _inferences.GetTriplesWithSubjectPredicate(qname(s), qname(p));
            var nobj = literal(_literal);
            return results1.Any(t => t.Object.Equals(nobj));
        }

        protected ILiteralNode literal(string value) => _defaultGraph.CreateLiteralNode(value);

        protected IUriNode node(string name) => _defaultGraph.CreateUriNode($"a:{name}");

        protected IUriNode qname(string name) => _defaultGraph.CreateUriNode(name);

        [SetUp]
        public void Setup()
        {
            SetupNamespaceMap();
            _tripleStore = new TripleStore();
            _defaultGraph = CreateGraph(null);
            _inferences = CreateGraph(new Uri("http://industrialinference.com/inferred/"));
            _contradictions = CreateGraph(new Uri("http://industrialinference.com/contradictions/"));
            _tripleStore.Add(_defaultGraph);
            _tripleStore.Add(_inferences);
            _tripleStore.Add(_contradictions);
            var qp = new LeviathanQueryProcessor(_tripleStore);
            var up = new LeviathanUpdateProcessor(_tripleStore);
            var builder = new ContainerBuilder();
            builder.RegisterType<InferenceEngine>().AsImplementedInterfaces();
            builder.Register<ISparqlQueryProcessor>(_ => qp);
            builder.Register<ISparqlUpdateProcessor>(_ => up);
            _container = builder.Build();
        }

        public TripleStore GetSoccerTripleStore()
        {
            var ts = new TripleStore();
            var g = new Graph();
            g.LoadFromEmbeddedResource("infer_tests.test_data.soccer.ttl");
            g.BaseUri = null;
            ts.Add(g, true);
            return ts;
        }
    }
}