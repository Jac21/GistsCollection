using WorkingWithNulls.AbstractClasses;

namespace WorkingWithNulls.Defenses
{
    public class DiamondSkinDefense : SpecialDefense
    {
        public override int CalculateDamageReduction(int totalDamage)
        {
            return 1;
        }
    }
}