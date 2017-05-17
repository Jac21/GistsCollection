using System;
using SwitchDemo.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OptionTests
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void WhenMatchesSome()
        {
            bool touched = false;
            Option<int>.Some(5).When(x => x > 3).Do(x => touched = true).Execute();
            Assert.IsTrue(touched);
        }

        [TestMethod]
        public void WhenDoesntMatchSome()
        {
            Option<int>.Some(5).When(x => x > 6).Do(x => Assert.Fail()).Execute();
        }

        [TestMethod]
        public void WhenDoesntFallThroughAfterFirstMatch()
        {
            Option<int>.Some(5).When(x => x > 3).Do(x => { }).WhenSome().Do(x => Assert.Fail()).Execute();
        }

        [TestMethod]
        public void WhenSomeMatched()
        {
            bool touched = false;
            Option<int>.Some(5).WhenSome().Do(x => touched = true).When(x => x > 3).Do(x => Assert.Fail()).Execute();
            Assert.IsTrue(touched);
        }

        [TestMethod]
        public void NoneOfWhenMatchesSome()
        {
            bool touched = false;
            Option<int>.Some(5).When(x => x > 6).Do(x => Assert.Fail()).When(x => x > 7).Do(x => Assert.Fail()).Execute();
            Assert.IsFalse(touched);
        }

        [TestMethod]
        public void WhenMatchesAndServesInputValueToLambda()
        {
            int value = 0;
            Option<int>.Some(5).When(x => x > 6).Do(x => Assert.Fail()).When(x => x > 3).Do(x => value = x).Execute();
            Assert.AreEqual(5, value);
        }

        [TestMethod]
        public void WhenSomeMatchesAndServesInputValueToLambda()
        {
            int value = 0;
            Option<int>.Some(5).When(x => x > 6).Do(x => Assert.Fail()).WhenSome().Do(x => value = x).Execute();
            Assert.AreEqual(5, value);
        }

        [TestMethod]
        public void WhenNotMatchingNone()
        {
            Option<int>.None().When(x => true).Do(x => Assert.Fail()).Execute();
        }

        [TestMethod]
        public void WhenNoneMatchesNone()
        {
            bool touched = false;
            Option<int>.None().When(x => true).Do(x => Assert.Fail()).WhenNone().Do(() => touched = true).Execute();
            Assert.IsTrue(touched);
        }

        [TestMethod]
        public void WhenNoneDosntFallThrough()
        {
            Option<int>.None().WhenNone().Do(() => { }).WhenNone().Do(() => Assert.Fail()).Execute();
        }

        [TestMethod]
        public void MapToExecutedOnSome()
        {
            string result = Option<int>.Some(5).When(x => x > 3).MapTo(x => $"{x} > 3").WhenSome().MapTo(x => "error").Map();
            Assert.AreEqual("5 > 3", result);
        }

        [TestMethod]
        public void MapToExecutedAfterWhenSome()
        {
            string result = Option<int>.Some(5).WhenSome().MapTo(x => $"{x} > 3").WhenSome().MapTo(x => "error").Map();
            Assert.AreEqual("5 > 3", result);
        }

        [TestMethod]
        public void MapToDoesntFallThrough()
        {
            string result = Option<int>.Some(5).When(x => x > 3).MapTo(x => $"{x} > 3").When(x => x > 2).MapTo(x => "error").Map();
            Assert.AreEqual("5 > 3", result);
        }

        [TestMethod]
        public void MapToMatchedOnNone()
        {
            string result = Option<int>.None().When(x => true).MapTo(x => "error").WhenNone().MapTo(() => "success").Map();
            Assert.AreEqual("success", result);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void UnmatchedMapToFailsOnSome()
        {
            Option<int>.Some(5).When(x => x > 6).MapTo(x => "error").WhenNone().MapTo(() => "error").Map();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnmatchedMapToFailOnNone()
        {
            Option<int>.None().WhenSome().MapTo(x => "error").Map();
        }
    }
}
