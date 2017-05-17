using System;

namespace RemovingNullChecks
{
    class TimeLimitedWarranty : IWarranty
    {
        private DateTime DateIssued { get; set; }
        private TimeSpan Duration { get; set; }

        public TimeLimitedWarranty(DateTime dateIssued, TimeSpan duration)
        {
            this.DateIssued = dateIssued.Date;
            this.Duration = TimeSpan.FromDays(duration.Days);
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
            return date.Date >= this.DateIssued && date.Date < this.DateIssued + this.Duration;
        }
    }
}