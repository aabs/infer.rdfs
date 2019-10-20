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
    /// <summary>
    /// test semantics of the inference engine by setting up antecedants and checking consequents of rules.
    /// </summary>
    /// <remarks>
    /// <para>Refer to <a href="https://www.w3.org/TR/rdf11-mt/#patterns-of-rdfs-entailment-informative">here</a> for more details about the inference rules</para>
    /// </remarks>

    public class RdfsEntailmentTests : RdfTestBase
    {
        #region rdfs2
        // # rdfs2 - attribution of type by property domain
        // 
        // if (?p rdfs:domain ?s. && ?s2 ?p ?o .)
        // then ?s2 rdf:type ?s .

        [Test]
        public void Test_rdfs2()
        {
            _assert("a:p", "rdfs:domain", "a:s");
            _assert("a:s2", "a:p", "a:o");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:s2", "rdf:type", "a:s"));
        }
        #endregion

        #region rdfs3
        // # rdfs3	
        // if (?p rdfs:range ?x  &&  ?y ?p ?z)
        // then ?z rdf:type ?x.

        [Test]
        public void Test_rdfs3()
        {
            _assert("a:p", "rdfs:range", "a:x");
            _assert("a:y", "a:p","a:z");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:z", "rdf:type", "a:x"));
        }
        #endregion

        #region rdfs4a
        /*
         * rdfs4a:	if(xxx aaa yyy ._ then (xxx rdf:type rdfs:Resource .)

         */
        [Test]
        public void Test_rdfs4a()
        {
            _assert("a:xxx", "a:aaa", "a:yyy");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdf:type", "rdfs:Resource"));
        }
        #endregion

        #region rdfs4b
        /*
         * rdfs4a:	if(xxx aaa yyy ._ then (xxx rdf:type rdfs:Resource .)

         */
        [Test]
        public void Test_rdfs4b()
        {
            _assert("a:xxx", "a:aaa", "a:yyy");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:yyy", "rdf:type", "rdfs:Resource"));
        }
        #endregion

        #region rdfs5
        /*
         * rdfs5:if (xxx rdfs:subPropertyOf yyy .&& yyy rdfs:subPropertyOf zzz .) then ( xxx rdfs:subPropertyOf zzz .)

         */
        [Test]
        public void Test_rdfs5()
        {
            _assert("a:xxx", "rdfs:subPropertyOf", "a:yyy");
            _assert("a:yyy", "rdfs:subPropertyOf", "a:zzz");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subPropertyOf", "a:zzz"));
        }
        #endregion
        #region rdfs6
        /*
         * rdfs6: if (xxx rdf:type rdf:Property .) then ( xxx rdfs:subPropertyOf xxx .)

         */
        [Test]
        public void Test_rdfs6()
        {
            _assert("a:xxx", "rdf:type", "rdf:Property");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subPropertyOf", "a:xxx"));
        }
        #endregion
        #region rdfs7
        /*
         * rdfs7: if ( aaa rdfs:subPropertyOf bbb . && xxx aaa yyy . ) then ( xxx bbb yyy .)
         */
        [Test]
        public void Test_rdfs7()
        {
            _assert("a:aaa", "rdfs:subPropertyOf", "a:bbb");
            _assert("a:xxx", "a:aaa", "a:yyy");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "a:bbb", "a:yyy"));
        }
        #endregion
        #region rdfs8
        /*
         * rdfs8: if ( xxx rdf:type rdfs:Class .) then ( xxx rdfs:subClassOf rdfs:Resource .)
         */
        [Test]
        public void Test_rdfs8()
        {
            _assert("a:xxx", "rdf:type", "rdfs:Class");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subClassOf", "rdfs:Resource"));
        }
        #endregion
        #region rdfs9
        /*
         * rdfs9: if ( xxx rdfs:subClassOf yyy . && zzz rdf:type xxx .) then ( zzz rdf:type yyy .)
         */
        [Test]
        public void Test_rdfs9()
        {
            _assert("a:xxx", "rdfs:subClassOf", "a:yyy");
            _assert("a:zzz", "rdf:type", "a:xxx");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:zzz", "rdf:type", "a:yyy"));
        }
        #endregion
        #region rdfs10
        /*
         * rdfs10: if (xxx rdf:type rdfs:Class .) then (xxx rdfs:subClassOf xxx .)
         */
        [Test]
        public void Test_rdfs10()
        {
            _assert("a:xxx", "rdf:type", "rdfs:Class");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subClassOf", "a:xxx"));
        }
        #endregion
        #region rdfs11
        /*
         * rdfs11: if ( xxx rdfs:subClassOf yyy . && yyy rdfs:subClassOf zzz .)	then ( xxx rdfs:subClassOf zzz .)

         */
        [Test]
        public void Test_rdfs11()
        {
            _assert("a:xxx", "rdfs:subClassOf", "a:yyy");
            _assert("a:yyy", "rdfs:subClassOf", "a:zzz");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subClassOf", "a:zzz"));
        }
        #endregion
        #region rdfs12
        /*
         * rdfs12: if ( xxx rdf:type rdfs:ContainerMembershipProperty .) then (	xxx rdfs:subPropertyOf rdfs:member .)
         */
        [Test]
        public void Test_rdfs12()
        {
            _assert("a:xxx", "rdf:type", "rdfs:ContainerMembershipProperty");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subPropertyOf", "rdfs:member"));
        }
        #endregion
        #region rdfs13
        /*
         * rdfs13: if (xxx rdf:type rdfs:Datatype .) then ( xxx rdfs:subClassOf rdfs:Literal .
         */
        [Test]
        public void Test_rdfs13()
        {
            _assert("a:xxx", "rdf:type", "rdfs:Datatype");
            var inf = _container.Resolve<IInferenceEngine>();
            inf.Infer(null);
            Assert.True(is_asserted("a:xxx", "rdfs:subClassOf", "rdfs:Literal"));
        }
        #endregion
    }
}
