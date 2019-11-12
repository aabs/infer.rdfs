using Inference.Storage;
using NUnit.Framework;
using Shouldly;
using AutoFixture;
using System;
using System.Linq;
using System.Diagnostics;

namespace Inference.Test
{
    [TestFixture]
    public class ComposedTripleStoreTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            // Add code that runs before each test method
            this._fixture = new AutoFixture.Fixture();
        }
        [Test]
        public void TestCanCreateEmptyStore()
        {
            var sut = new ComposedTripleStore();
            sut.ShouldNotBeNull();
        }
        [Test]
        public void TestCanAddATriple()
        {
            var sut = new ComposedTripleStore();
            var ord = sut.InsertTriple(_fixture.Create<Triple>());
            ord.ShouldBe(0);
        }

        [Test]
        public void TestCanAddATripleAndGetBackViaRandomAccess()
        {
            var sut = new ComposedTripleStore();
            Triple t = _fixture.Create<Triple>();
            var ord = sut.InsertTriple(t);
            var t2 = sut.ElementAt(ord);
            t.ShouldBe(t2);
        }

        [Test]
        public void TestCanAddATripleAndGetBackViaRandomAccessOnLargeList()
        {
            var sut = new ComposedTripleStore();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        sut.InsertTriple(new Triple(_fixture.Create<Uri>(), new Uri($"urn:{j}"), _fixture.Create<Uri>()));
                    }
                }
            }
            // should be 1000 triples spread over 10 PropertyTables

            var t2 = sut.ElementAt(500);
            t2.ShouldNotBeNull();
            foreach (var triple in sut.Skip(482).Take(20))
            {
                Debug.WriteLine($"<{triple.Subject}> <{triple.Predicate}> <{triple.Object}> .");
            }
        }
    }
}