using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyExpressionTree.Tests
{
    [TestClass]
    public class MyExpressionTreeUnitTests
    {
        [TestMethod]
        public void ExpressionTreeFilterTestWithSingleResultSuccess()
        {
            // arrange

            // Specify the property to filter
            string propertyName = "LastName";

            // Value to search against the above property
            string value = "Curry";

            // act
            var dn = ExpressionTree.GetDynamicQueryWithExpressionTrees(propertyName, value);
            var output = Utilities.DataSeeder.UserDataSeed().Where(dn).ToList();

            // assert
            Assert.IsNotNull(output);
            Assert.AreEqual(output.FirstOrDefault()?.Id, 2);
            Assert.AreEqual(output.FirstOrDefault()?.FirstName, "Stephen");
            Assert.AreEqual(output.FirstOrDefault()?.LastName, "Curry");
        }

        [TestMethod]
        public void ExpressionTreeFilterTestWithMultipleResultsSuccess()
        {
            // arrange

            // Specify the property to filter
            string propertyName = "LastName";

            // Value to search against the above property
            string value = "Durant";

            // act
            var dn = ExpressionTree.GetDynamicQueryWithExpressionTrees(propertyName, value);
            var output = Utilities.DataSeeder.UserDataSeed().Where(dn).ToList();

            // assert
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count == 2);
            Assert.IsTrue(output.All(x => x.LastName == "Durant"));
        }

        [TestMethod]
        public void ExpressionTreeFilterTestWithNoResultsEmptyReturn()
        {
            // arrange

            // Specify the property to filter
            string propertyName = "LastName";

            // Value to search against the above property
            string value = "Windings";

            // act
            var dn = ExpressionTree.GetDynamicQueryWithExpressionTrees(propertyName, value);
            var output = Utilities.DataSeeder.UserDataSeed().Where(dn).ToList();

            // assert
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count == 0);
        }
    }
}