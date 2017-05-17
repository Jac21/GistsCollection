using System;

namespace RemovingNullChecks
{
    internal interface IWarranty
    {
        void Claim(DateTime onDate, Action onValidClaim);
    }
}