using System.Collections.Generic;
using System.Linq;
using AutoFixtureTestingContext.Repositories;
using AutoFixtureTestingContext.Services;
using NUnit.Framework;

namespace AutoFixtureTestingContext.Tests
{
    [TestFixture]
    public class TestServiceTests : TestingContext<TestService>
    {
        [SetUp]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public void Setup()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            base.Setup();
        }

        [Test]
        public void WhenGetNamesExceptJeremyCalled_JeremyIsNotReturned()
        {
            GetMockFor<ITestRepository>()
                .Setup(x => x.GetNames()).Returns(new List<string> { "Racecar", "Jackson"});

            var result = ClassUnderTest.GetNamesExceptJeremy();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Racecar", result.FirstOrDefault());
        }

        [Test]
        public void WhenPalindromeNames_OnlyPalindromeNamesReturned()
        {
            GetMockFor<ITestRepository>().Setup(x => x.GetNames()).Returns(new List<string> { "Racecar", "Jackson"});
            InjectClassFor(new UtilityService());

            var result = ClassUnderTest.GetPalindromeNames();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Racecar", result.First());
        }
    }
}