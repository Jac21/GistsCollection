using System;

namespace MyFluentInterface.Utilities
{
    public static class FnUtils
    {
        public static Func<TA, TC> Compose<TA, TB, TC>(Func<TA, TB> f1, Func<TB, TC> f2)
        {
            return a => f2(f1(a));
        }
    }
}