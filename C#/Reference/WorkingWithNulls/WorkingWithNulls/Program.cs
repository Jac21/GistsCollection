using System;
using WorkingWithNulls.AbstractClasses;
using WorkingWithNulls.Defenses;

namespace WorkingWithNulls
{
    class Program
    {
        static void Main()
        {
            var playerOne = new PlayerCharacter(new DiamondSkinDefense())
            {
                Name = "Jeremy"
            };
            
            var playerTwo = new PlayerCharacter(new IronBonesDefence()){
                Name = "Jac",
                DateOfBirth =  DateTime.Now,
                DaysSinceLastLogin = 5,
                Health = 100,
                IsNoob = true
            };
            
            var playerThree = new PlayerCharacter(SpecialDefense.Null)
            {
                Name = "John"
            };

            playerOne.Hit(10);
            playerTwo.Hit(10);
            playerThree.Hit(10);

            PlayerDisplayer.Write(playerOne);
            PlayerDisplayer.Write(playerTwo);
            PlayerDisplayer.Write(playerThree);
        }
    }
}
