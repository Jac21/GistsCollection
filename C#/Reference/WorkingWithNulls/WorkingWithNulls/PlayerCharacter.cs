using System;
using WorkingWithNulls.AbstractClasses;

namespace WorkingWithNulls
{
    class PlayerCharacter
    {
        public string Name { get; set; }
        public int? DaysSinceLastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsNoob { get; set; }

        private readonly SpecialDefense _specialDefense;
        public int Health { get; set; }

        public PlayerCharacter(SpecialDefense specialDefense)
        {
            // implementing using Nullable<T>
            DaysSinceLastLogin = null;
            DateOfBirth = null;

            this._specialDefense = specialDefense;
            this.Health = 100;
        }

        public void Hit(int damage)
        {
            int totalDamageTaken = damage - _specialDefense.CalculateDamageReduction(damage);

            Health -= totalDamageTaken;

            Console.WriteLine("{0}'s health has been reduced by {1} to {2}", 
                this.Name, totalDamageTaken, this.Health);
        }
    }
}
