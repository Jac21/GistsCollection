using WorkingWithNulls.AbstractClasses;

namespace WorkingWithNulls.Defenses
{
    public class IronBonesDefence : SpecialDefense
    {
        public override int CalculateDamageReduction(int totalDamage)
        {
            return 5;
        }
    }
}