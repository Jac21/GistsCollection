using System;

namespace RemovingNullChecks
{
    /// <summary>
    /// "Special Case" object, shouldn't really know
    /// too much about external state (e.g., user info),
    /// although can have some behavior (if domain object)
    /// </summary>
    class LifetimeWarranty : IWarranty
    {
        private DateTime IssuingDate { get; set; }

        public LifetimeWarranty(DateTime issuingDate)
        {
            this.IssuingDate = issuingDate.Date;
        }

        public void Claim(DateTime onDate, Action onValidClaim)
        {
            if (!this.IsValidOn(onDate))
            {
                return;
            }

            onValidClaim();
        }

        private bool IsValidOn(DateTime date)
        {
            return date.Date >= this.IssuingDate;
        }
    }
}