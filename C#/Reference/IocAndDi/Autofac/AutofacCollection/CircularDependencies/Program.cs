using System;
using Autofac;

namespace CircularDependencies
{
    /// <summary>
    /// When you have one class that takes a property dependency of a second type,
    /// and the second type has a property dependency of the first type
    /// </summary>
    public class Program
    {
        private static IContainer Container { get; set; }

        /// <summary>
        /// 1. Make the property dependencies settable (writable)
        /// 2. Register the types using PropertiesAutowired
        /// 3. Neither type can be registered as InstancePerDependency. If either type
        /// is set to factory scope, results will be invalid
        /// </summary>
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<DependsByProp1>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder
                .RegisterType<DependsByProp2>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder
                .RegisterType<DependsByCtor>()
                .InstancePerLifetimeScope();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var prop1 = scope.Resolve<DependsByProp1>();
                prop1.Execute();
                prop1.Dependency.Execute();

                var propCtor = scope.Resolve<DependsByCtor>();
                propCtor.Execute();
            }
        }
    }

    public class DependsByProp1
    {
        public DependsByProp2 Dependency { get; set; }

        public void Execute()
        {
            Console.WriteLine("Prop1 Executed!");
        }
    }

    public class DependsByProp2
    {
        public DependsByProp1 Dependency { get; set; }

        public void Execute()
        {
            Console.WriteLine("Prop2 Executed!");
        }
    }

    public class DependsByCtor
    {
        public DependsByCtor(DependsByProp1 dependency)
        {
            Console.Write("Depends by Ctor: ");
            dependency.Execute();
        }

        public void Execute()
        {
            Console.WriteLine("PropCtor Executed!");
        }
    }
}