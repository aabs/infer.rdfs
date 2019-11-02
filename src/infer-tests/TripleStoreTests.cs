using Inference.Storage;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace Inference.Test
{
    [TestFixture]
    public class TripleStoreTests
    {
        [Test]
        public void TestCanCreateTripleStore()
        {
            var sut = new TripleCollection();
            sut.ShouldNotBeNull();
        }

        [Test]
        public void TestCanCreateTriple()
        {
            var sut = new TripleCollection();
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
            var sut = new TripleCollection();
            Uri s = new Uri("urn:1");
            Uri p = new Uri("urn:2");
            Uri o = new Uri("urn:3");
            Triple t = sut.NewTriple(s, p, o);
            sut.InsertTriple(t);
        }

        [Test]
        public void TestCanRetrieveTriple()
        {
            var sut = new TripleCollection();
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
        public void TestCanStoreAndRetrieveManyTriples()
        {
            var sut = new TripleCollection();
            for (int i = 0; i < 10000; i++)
            {
                Uri s = new Uri($"urn:{i}");
                Uri p = new Uri($"urn:{i + 1}");
                Uri o = new Uri($"urn:{i + 2}");
                Triple t = sut.NewTriple(s, p, o);
                sut.InsertTriple(t);
            }
            Triple t2 = sut.ElementAt(500);
            t2.ShouldNotBeNull();
            t2.Subject.ShouldBe(new Uri($"urn:500"));
            t2.Predicate.ShouldBe(new Uri($"urn:501"));
            t2.Object.ShouldBe(new Uri($"urn:502"));
        }
        [Test]
        public void TestCanStoreAndEnumerateManyTriples()
        {
            var sut = new TripleCollection();
            for (int i = 0; i < 100; i++)
            {
                Uri s = new Uri($"urn:{i}");
                Uri p = new Uri($"urn:{i + 1}");
                Uri o = new Uri($"urn:{i + 2}");
                Triple t = sut.NewTriple(s, p, o);
                sut.InsertTriple(t);
            }

            int ct = 0;
            foreach (var t in sut)
            {
                ct++;
                if (ct % 20 == 0)
                {
                    Debug.WriteLine($"<{t.Subject}> <{t.Predicate}> <{t.Object}> .");
                }
            }
        }
        [Test]
        public void TestCanStoreAndEnumerateManyCommonTriples()
        {
            Uri pred = new Uri($"urn:common-predicate");
            var predId = RdfCompressionContext.Instance.UriRegistry.Add(pred);
            var sut = new CommonPredicateTripleCollection(predId);
            for (int i = 0; i < 100; i++)
            {
                Uri s = new Uri($"urn:{i}");
                Uri p = pred;
                Uri o = new Uri($"urn:{i + 2}");
                Triple t = sut.NewTriple(s, p, o);
                sut.InsertTriple(t);
            }

            int ct = 0;
            foreach (var t in sut)
            {
                ct++;
                if (ct % 20 == 0)
                {
                    Debug.WriteLine($"<{t.Subject}> <{t.Predicate}> <{t.Object}> .");
                }
            }
        }
    }
}