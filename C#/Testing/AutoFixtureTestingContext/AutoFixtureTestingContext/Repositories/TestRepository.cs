using System.Collections.Generic;

namespace AutoFixtureTestingContext.Repositories
{
    public interface ITestRepository
    {
        List<string> GetNames();
    }

    public class TestRepository : ITestRepository
    {
        public List<string> GetNames()
        {
            return new List<string>
            {
                "Racecar",
                "Jeremy",
                "Jackson"
            };
        }
    }
}