using AutoFixture;
using AutoFixture.AutoMoq;
using Inference.Core.Inference;
using NUnit.Framework;
using Shouldly;
using VDS.RDF.Query;
using VDS.RDF.Update;

namespace Inference.Test
{
    [TestFixture]
    internal class TestLocalInference
    {
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            // Add code that runs before each test method
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void TestCanCreateLocalInferenceEngine()
        {
            var qp = fixture.Create<ISparqlQueryProcessor>();
            var up = fixture.Create<ISparqlUpdateProcessor>();
            var sut = new LocalInferenceEngine(qp, up);
            sut.ShouldNotBeNull();
        }

        [Test]
        public void TestCanLoadRules()
        {
            var qp = fixture.Create<ISparqlQueryProcessor>();
            var up = fixture.Create<ISparqlUpdateProcessor>();
            var sut = new LocalInferenceEngine(qp, up);
            var rules = sut.LoadRules();
            rules.ShouldNotBeNull();
            rules.ShouldNotBeEmpty();
        }
    }
}