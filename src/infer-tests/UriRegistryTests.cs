﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using triple_store;

namespace infer_tests
{
    [TestFixture]
    public class UriRegistryTests
    {
        [Test]
        public void TestCanCreateRegistry()
        {
            var sut = new UriRegistry();
            sut.ShouldNotBeNull();
        }
        [Test]
        public void TestCanRegisterUri()
        {
            var sut = new UriRegistry();
            var id = sut.Add(new Uri("http://www.example.com/1"));
            id.ShouldBe(1);
            var id2 = sut.Add(new Uri("http://www.example.com/2"));
            id2.ShouldBe(2);
        }

        [Test]
        public void TestCanLookupRegisteredUris()
        {
            var sut = new UriRegistry();
            sut.Add(new Uri("http://www.example.com/1"));
            sut.Add(new Uri("http://www.example.com/2"));
            var id = sut.Get(new Uri("http://www.example.com/1"));
            var id2 = sut.Get(new Uri("http://www.example.com/2"));
            id.ShouldBe(1);
            id2.ShouldBe(2);
        }
    }
}
