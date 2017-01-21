using System;

namespace RemovingNullChecks
{
    class Program
    {
        static void ClaimWarranty(SoldArticle article)
        {
            DateTime now = DateTime.Now;

            article.MoneyBackGuarantee.Claim(now, 
                () => Console.WriteLine("Offer money back"));

            article.ExpressWarranty.Claim(now,
                () => Console.WriteLine("Offer repair."));
        }

        static void Main()
        {
            DateTime sellingDate = new DateTime(2016, 8, 9);
            TimeSpan moneyBackSpan = TimeSpan.FromDays(30);
            TimeSpan warrantySpan = TimeSpan.FromDays(365);

            IWarranty moneyBack = new TimeLimitedWarranty(sellingDate, moneyBackSpan);
            IWarranty warranty = new TimeLimitedWarranty(sellingDate, warrantySpan);
            IWarranty lifetimeWarranty = new LifetimeWarranty(sellingDate);

            SoldArticle goods = new SoldArticle(VoidWarranty.Instance, warranty);

            ClaimWarranty(goods);

            Console.ReadLine();
        }
    }
}
