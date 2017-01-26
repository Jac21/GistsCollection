using System;

namespace SwitchDemo
{
    class LifetimeWarranty: IWarranty
    {
        private DateTime IssuingDate { get; }

        public LifetimeWarranty(DateTime issuingDate)
        {
            this.IssuingDate = issuingDate.Date;
        }

        public void Claim(DateTime onDate, Action onValidClaim)
        {
            if (!IsValidOn(onDate))
                return;
            onValidClaim();
        }

        private bool IsValidOn(DateTime date) =>
            date.Date >= this.IssuingDate;

    }
}
