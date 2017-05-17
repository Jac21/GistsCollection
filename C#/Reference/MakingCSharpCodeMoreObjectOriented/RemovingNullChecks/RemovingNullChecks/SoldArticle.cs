using System;

namespace RemovingNullChecks
{
    class SoldArticle
    {
        public IWarranty MoneyBackGuarantee { get; private set; }
        public IWarranty ExpressWarranty { get; private set; }
        private IWarranty NotOperationalWarranty { get; set; }

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            // make sure props never return null
            if (moneyBack == null)
            {
                throw new ArgumentNullException();
            }

            if (express == null)
            {
                throw new ArgumentNullException();
            }

            this.MoneyBackGuarantee = moneyBack;
            this.ExpressWarranty = VoidWarranty.Instance;
            this.NotOperationalWarranty = express;
        }

        public void VisibleDamage()
        {
            this.MoneyBackGuarantee = VoidWarranty.Instance;
        }

        public void NotOperational()
        {
            this.MoneyBackGuarantee = VoidWarranty.Instance;
            this.ExpressWarranty = this.NotOperationalWarranty;
        }
    }
}