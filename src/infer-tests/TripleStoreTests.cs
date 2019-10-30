using NUnit.Framework;
using Shouldly;
using System;
using triple_store;

namespace infer_tests
{
    [TestFixture]
    public class TripleStoreTests
    {
        [Test]
        public void TestCanCreateTripleStore()
        {
            var sut = new TripleStore();
            sut.ShouldNotBeNull();
        }

        [Test]
        public void TestCanCreateTriple()
        {
            var sut = new TripleStore();
            Uri s = new Uri("urn:1");
            Uri p = new Uri("urn:2");
            Uri o = new Uri("urn:3");
            Triple t = sut.NewTriple(s, p, o);
            t.ShouldNotBeNull();
            t.Subject.ShouldBe(s);
            t.Predicate.ShouldBe(p);
            t.Object.ShouldBe(o);
        }

        [Test]
        public void TestCanStoreTriple()
        {
            var sut = new TripleStore();
            Uri s = new Uri("urn:1");
            Uri p = new Uri("urn:2");
            Uri o = new Uri("urn:3");
            Triple t = sut.NewTriple(s, p, o);
            sut.InsertTriple(t);
        }

        [Test]
        public void TestCanRetrieveTriple()
        {
            var sut = new TripleStore();
            Uri s = new Uri("urn:1");
            Uri p = new Uri("urn:2");
            Uri o = new Uri("urn:3");
            Triple t = sut.NewTriple(s, p, o);
            int x = sut.InsertTriple(t);
            Triple t2 = sut.ElementAt(x);
            t2.ShouldNotBeNull();
            t2.Subject.ShouldBe(s);
            t2.Predicate.ShouldBe(p);
            t2.Object.ShouldBe(o);
        }

        [Test]
        public void TestCanStoreAndRetrieveManyTriple()
        {
            var sut = new TripleStore();
            for (int i = 0; i < 10000; i++)
            {
                Uri s = new Uri($"urn:{i}");
                Uri p = new Uri($"urn:{i+1}");
                Uri o = new Uri($"urn:{i+2}");
                Triple t = sut.NewTriple(s, p, o);
                sut.InsertTriple(t);
            }
            Triple t2 = sut.ElementAt(500);
            t2.ShouldNotBeNull();
            t2.Subject.ShouldBe(new Uri($"urn:500"));
            t2.Predicate.ShouldBe(new Uri($"urn:501"));
            t2.Object.ShouldBe(new Uri($"urn:502"));
        }
    }
}