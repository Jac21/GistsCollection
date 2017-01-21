using System;

namespace RemovingNullChecks
{
    /// <summary>
    /// Null object, exposes certain interface, 
    /// but does nothing (use instead of a null).
    /// Usually implemented as a singleton.
    /// </summary>
    class VoidWarranty : IWarranty
    {
        [ThreadStatic]
        private static VoidWarranty instance;

        private VoidWarranty() { }

        public static VoidWarranty Instance
        {
            get { return instance ?? (instance = new VoidWarranty()); }
        }

        // Do nothing
        public void Claim(DateTime onDate, Action onValidClaim) { }
    }
}