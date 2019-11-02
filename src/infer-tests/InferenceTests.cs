using Autofac;
using Inference.Core;
using NUnit.Framework;
using System.Linq;

namespace Inference.Test
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