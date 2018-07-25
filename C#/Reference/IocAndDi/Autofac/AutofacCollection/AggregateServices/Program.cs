using System;
using Autofac;
using Autofac.Extras.AggregateService;

namespace AggregateServices
{
    public class Program
    {
        public static IContainer Container { get; set; }
        public static IComponentContext ComponentContext { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();

            // we register the aggregate service interface
            builder.RegisterAggregateService<IMyAggregateService>();
            builder.RegisterType<FirstService>().As<IFirstService>();
            builder.RegisterType<SecondService>().As<ISecondService>();
            builder.RegisterType<ThirdService>().As<IThirdService>();
            builder.RegisterType<FourthService>().As<IFourthService>();

            builder.RegisterType<MyService>().As<IMyService>();
            builder.RegisterType<SomeThirdService>().As<ISomeThirdService>();

            //var assembly = Assembly.GetExecutingAssembly();
            //var dependencies = assembly.GetTypes().Where(t => t.IsInterface && t.Name == "IMyAggregateService" && t.IsNested);
            //foreach (var dependency in dependencies)
            //{
            //    builder.RegisterAggregateService(dependency);
            //}

            Container = builder.Build();
            ComponentContext = Container;

            /*******************************************************************************************************/
            MyAggregateServicePropertyImpl myAggregateServicePropertyImpl =
                new MyAggregateServicePropertyImpl(ComponentContext);
            myAggregateServicePropertyImpl.MyService.DoWork();

            MyAggregateServiceMethodImpl myAggregateServiceMethodImpl =
                new MyAggregateServiceMethodImpl(ComponentContext);
            var someThirdService = myAggregateServiceMethodImpl.GetThirdService("data");
            someThirdService.DoWork();
            myAggregateServiceMethodImpl.FirstServiceDoWork();
        }
    }

    /// <summary>
    /// Methods will behave like factory delegates and will translate into a resolve call on each invocation. 
    /// The method return type will be resolved, passing on any parameters to the resolve call.
    /// </summary>
    public class MyAggregateServiceMethodImpl : IMyAggregateService
    {
        private readonly IComponentContext context;

        public MyAggregateServiceMethodImpl(IComponentContext context)
        {
            this.context = context;
        }

        public ISomeThirdService GetThirdService(string data)
        {
            var dataParam = new TypedParameter(typeof(string), data);
            return context.Resolve<ISomeThirdService>(dataParam);
        }

        public void FirstServiceDoWork()
        {
            context.Resolve<IFirstService>().DoWork();
        }

        public IFirstService FirstService { get; }
        public ISecondService SecondService { get; }
        public IThirdService ThirdService { get; }
        public IFourthService FourthService { get; }
    }

    public interface ISomeThirdService
    {
        void DoWork();
    }

    internal class SomeThirdService : ISomeThirdService
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }

    /// <summary>
    /// Read-only properties mirror the behavior of regular constructor-injected dependencies. The type of each 
    /// property will be resolved and cached in the aggregate service when the aggregate service instance is constructed.
    /// </summary>
    public class MyAggregateServicePropertyImpl : IMyAggregateService
    {
        public IMyService MyService { get; }

        public MyAggregateServicePropertyImpl(IComponentContext context)
        {
            MyService = context.Resolve<IMyService>();
        }

        public IFirstService FirstService { get; }
        public ISecondService SecondService { get; }
        public IThirdService ThirdService { get; }
        public IFourthService FourthService { get; }
    }

    public interface IMyService
    {
        void DoWork();
    }

    internal class MyService : IMyService
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }

    /// <summary>
    /// To aggregate the dependencies we move those into a separate interface definition and take a dependency on that interface instead.
    /// </summary>
    public class SomeController
    {
        private readonly IMyAggregateService aggregateService;

        public SomeController(IMyAggregateService aggregateService)
        {
            this.aggregateService = aggregateService;
        }

        public void DoAllControllerWork()
        {
            aggregateService.FirstService.DoWork();
            aggregateService.SecondService.DoWork();
            aggregateService.ThirdService.DoWork();
            aggregateService.FourthService.DoWork();
        }
    }

    public interface IMyAggregateService
    {
        IFirstService FirstService { get; }
        ISecondService SecondService { get; }
        IThirdService ThirdService { get; }
        IFourthService FourthService { get; }
    }

    public interface IFirstService
    {
        void DoWork();
    }

    public interface ISecondService
    {
        void DoWork();
    }

    public interface IThirdService
    {
        void DoWork();
    }

    public interface IFourthService
    {
        void DoWork();
    }

    internal class FirstService : IFirstService
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }

    internal class SecondService : ISecondService
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }

    internal class ThirdService : IThirdService
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }

    internal class FourthService : IFourthService
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }
}