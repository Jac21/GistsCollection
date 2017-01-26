using System;

namespace OptionDemo
{
    interface IWarranty
    {
        void Claim(DateTime onDate, Action onValidClaim);
    }
}