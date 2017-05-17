using System;

namespace SwitchDemo
{
    class TimeLimitedWarranty : IWarranty
    {
        private DateTime DateIssued { get; }
        private TimeSpan Duration { get; }

        public TimeLimitedWarranty(DateTime dateIssued, TimeSpan duration)
        {
            this.DateIssued = dateIssued.Date;
            this.Duration = TimeSpan.FromDays(duration.Days);
        }

        public void Claim(DateTime onDate, Action onValidClaim)
        {
            if (!IsValidOn(onDate))
                return;
            onValidClaim();
        }

        private bool IsValidOn(DateTime date) =>
            date.Date >= this.DateIssued && date.Date < this.DateIssued + this.Duration;

    }
}
