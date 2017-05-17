namespace WorkingWithNulls.AbstractClasses
{
    public abstract class SpecialDefense
    {
        public abstract int CalculateDamageReduction(int totalDamage);

        public static SpecialDefense Null = new NullDefence();

        private class NullDefence : SpecialDefense
        {
            public override int CalculateDamageReduction(int totalDamage)
            {
                return 0; // "do nothing" behavior
            }
        }
    }
}
