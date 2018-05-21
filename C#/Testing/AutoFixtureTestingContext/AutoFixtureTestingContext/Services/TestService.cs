using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixtureTestingContext.Repositories;

namespace AutoFixtureTestingContext.Services
{
    public interface ITestService
    {
        List<string> GetNamesExceptJeremy();
    }

    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;
        private readonly UtilityService utilityService;

        public TestService(ITestRepository testRepository, UtilityService utilityService)
        {
            this.testRepository = testRepository;
            this.utilityService = utilityService;
        }

        public List<string> GetNamesExceptJeremy()
        {
            return testRepository.GetNames().Where(n => !n.Equals("jeremy", StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public List<string> GetPalindromeNames()
        {
            return testRepository.GetNames().Where(x => utilityService.IsPalindrome(x)).ToList();
        }
    }

    public class UtilityService
    {
        public bool IsPalindrome(string word)
        {
            return word.ToLowerInvariant() == string.Join(string.Empty, word.ToLowerInvariant().ToCharArray().Reverse());
        }
    }
}