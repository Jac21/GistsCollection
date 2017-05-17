using System;

namespace WorkingWithNulls
{
    class PlayerDisplayer
    {
        public static void Write(PlayerCharacter player)
        {
            // Instantiate variables for player name, login status, D.O.B., Newbie status
            var playerName = player.Name;

            //// Get value version with default value overload
            //var daysSinceLastLogin = player.DaysSinceLastLogin.GetValueOrDefault(-1);

            //// Using a conditional operator
            // var daysSinceLastLogin = player.DaysSinceLastLogin.HasValue ? player.DaysSinceLastLogin.Value : -1;
            
            //// Using a null-coalescing operator
            var daysSinceLastLogin = player.DaysSinceLastLogin ?? -1;
            
            var dateOfBirth = player.DateOfBirth ?? DateTime.MinValue;
            var noobStatus = player.IsNoob;

            // Name check
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Player name is null or whitespace");
            }
            else
            {
                Console.WriteLine("Name: {0}", playerName);
            }

            // Days since last login print
            Console.WriteLine("Days since last login: {0}", daysSinceLastLogin);

            // D.O.B.
            Console.WriteLine("Player date of birth: {0}", dateOfBirth);

            // IsNoob?
            if (!noobStatus.HasValue)
            {
                Console.WriteLine("Player newbie status is unknown");
            }
            else if (noobStatus == true)
            {
                Console.WriteLine("Player is noob");
            }
            else
            {
                Console.WriteLine("Player is experienced");
            }
        }
    }
}
