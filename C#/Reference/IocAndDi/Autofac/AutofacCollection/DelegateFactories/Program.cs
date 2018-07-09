using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace DelegateFactories
{
    public class Program
    {
        private static IContainer Container { get; set; }

        /// <summary>
        /// Factory adapters provide the instantiation features of the container to managed components 
        /// without exposing the container itself to them.
        /// If type T is registered with the container, Autofac will automatically resolve dependencies 
#pragma warning disable 1570
        /// on Func<T> as factories that create T instances through the container.</T>
#pragma warning restore 1570
        /// </summary>
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Shareholding>();
            builder.RegisterType<Portfolio>();

            // An implementor of IQuoteService can be registered through the container:
            builder.RegisterType<WebQuoteService>().As<IQuoteService>();

            Container = builder.Build();

            Calculate();
        }

        private static void Calculate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                /*
                 * Autofac will transparently add the extra parameter to the Shareholding constructor when a factory delegate is called.
                 * This means that Portfolio can take advantage of the Shareholding.Value property without knowing that a
                 * quote service is involved at all.
                 */
                var shareholdingFactory = scope.Resolve<Shareholding.Factory>();
                var shareholding = shareholdingFactory.Invoke("ShareholdingSymbol", 999);
                Console.WriteLine(shareholding.Value);

                var portfolio = scope.Resolve<Portfolio>();
                portfolio.Add("PortfolioSymbol", 4321);
                Console.WriteLine(portfolio.Value);
            }
        }
    }

    /// <summary>
    /// The Shareholding class declares a constructor, but also provides a delegate type that can be used to create Shareholdings indirectly.
    /// Autofac can make use of this to automatically generate a factory that can be accessed through the container:
    /// </summary>
    public class Shareholding
    {
        public delegate Shareholding Factory(string symbol, uint holding);

        public string Symbol { get; set; }
        public uint Holding { get; set; }
        private IQuoteService QuoteService { get; }

        /// <summary>
        /// We can add a value member to the Shareholding class that makes use of the service:
        /// </summary>
        public decimal Value
            => QuoteService.GetQuote(Symbol) * Holding;

        public Shareholding(string symbol, uint holding, IQuoteService quoteService)
        {
            Symbol = symbol;
            Holding = holding;
            QuoteService = quoteService;
        }
    }

    /// <summary>
    /// Other components can use the factory:
    /// </summary>
    public class Portfolio
    {
        private Shareholding.Factory ShareholdingFactory { get; }
        private readonly IList<Shareholding> holdings = new List<Shareholding>();

        public decimal Value => holdings.Aggregate(0m, (a, e) => a + e.Value);

        public Portfolio(Shareholding.Factory shareholdingFactory)
        {
            ShareholdingFactory = shareholdingFactory;
        }

        public void Add(string symbol, uint holding)
        {
            holdings.Add(ShareholdingFactory(symbol, holding));
        }
    }

    /// <summary>
    /// The Payoff
    /// </summary>
    public interface IQuoteService
    {
        decimal GetQuote(string symbol);
    }

    internal class WebQuoteService : IQuoteService
    {
        public decimal GetQuote(string symbol)
        {
            return 5m;
        }
    }
}