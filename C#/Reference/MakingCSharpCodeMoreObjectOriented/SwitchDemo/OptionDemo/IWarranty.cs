using System;

namespace SwitchDemo
{
    interface IWarranty
    {
        void Claim(DateTime onDate, Action onValidClaim);
    }
}