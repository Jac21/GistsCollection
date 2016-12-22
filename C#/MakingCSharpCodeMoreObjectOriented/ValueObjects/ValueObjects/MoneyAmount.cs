using System.Text;

namespace ValueObjects
{
    sealed class MoneyAmount
    {
        public decimal Amount { get; set; }
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Immutable class, instance cannot be changed after it has
        /// been created, impossible to reproduct an aliasing bug
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currencySymbol"></param>
        public MoneyAmount(decimal amount, string currencySymbol)
        {
            this.Amount = amount;
            this.CurrencySymbol = currencySymbol;
        }

        public MoneyAmount Scale(decimal factor)
        {
            return new MoneyAmount(this.Amount * factor, this.CurrencySymbol);
        }

        // Can also provide own operator delegate when working on types
        // that would benefit from it, such as money
        // public static MoneyAmount operator *(MoneyAmount amount, decimal factor) => amount.Scale(factor);

        public override string ToString()
        {
           return new StringBuilder()
               .AppendFormat("{0} {1}", this.Amount, this.CurrencySymbol)
               .ToString();
        }
    }
}
