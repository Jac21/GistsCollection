using System.Collections.Generic;
using MyExpressionTree.Models;

namespace MyExpressionTree.Utilities
{
    public static class DataSeeder
    {
        public static List<User> UserDataSeed()
        {
            return new List<User>
            {
                new User {Id = 1, FirstName = "Kevin", LastName = "Garnett"},
                new User {Id = 2, FirstName = "Stephen", LastName = "Curry"},
                new User {Id = 3, FirstName = "Kevin", LastName = "Durant"},
                new User {Id = 4, FirstName = "Copy", LastName = "Durant"}
            };
        }
    }
}