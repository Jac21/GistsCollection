using System;

namespace SwitchDemo
{
    class VoidWarranty: IWarranty
    {
        [ThreadStatic]
        private static VoidWarranty instance;

        private VoidWarranty() { }

        public static VoidWarranty Instance
        {
            get
            {
                if (instance == null)
                    instance = new VoidWarranty();
                return instance;
            }
        }

        public void Claim(DateTime onDate, Action onValidClaim) { }

    }
}
