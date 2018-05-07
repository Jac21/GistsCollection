using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Autofac;
using Autofac.Features.Metadata;
using Autofac.Extras.AttributeMetadata;
using Autofac.Features.AttributeFilters;
using IContainer = Autofac.IContainer;

namespace Metadata
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new ScreenAppender())
                .As<ILogAppender>()
                .WithMetadata<AppenderMetadata>(m =>
                    m.For(am => am.AppenderName, "screen"));

            // Step 4. Ensure the Container Uses Your Attributes
            // register the service to consume with metadata
            // Need to register AttributedMetadataModule since using
            // attributed metadata
            builder.RegisterModule<AttributedMetadataModule>();
            builder.RegisterType<CenturyArtwork>().As<IArtwork>();
            builder.RegisterType<QuarterCenturyArtwork>().As<IArtwork>();

            // specify WithAttributeFilter for the consumer
            builder.RegisterType<ArtDisplay>().As<IDisplay>().WithAttributeFiltering();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var screenerAppender = scope.Resolve<ILogAppender>();
                screenerAppender.Write("ScreenAppender executed!");

                scope.Resolve<IDisplay>();
            }
        }
    }

    public interface ILogAppender
    {
        void Write(string message);
    }

    public class ScreenAppender : ILogAppender
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Consuming metadata
    /// </summary>
    public class Log : ILogAppender
    {
        public readonly IEnumerable<Meta<ILogAppender>> Appenders;

        public Log(IEnumerable<Meta<ILogAppender>> appenders)
        {
            Appenders = appenders;
        }

        public void Write(string destination, string message)
        {
            var appender = Appenders.First(a => a.Metadata["AppenderName"].Equals(destination));
            appender.Value.Write(message);
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Strongly-Typed Metadata
    /// </summary>
    public class AppenderMetadata
    {
        [DefaultValue("screen")] public string AppenderName { get; set; }

        public AppenderMetadata(IDictionary<string, object> metadata)
        {
            AppenderName = (string) metadata["AppenderName"];
        }
    }

    // Attribute-Based Metadata

    /// <summary>
    /// Step 1. Create Your Metadata Attribute
    /// </summary>
    [MetadataAttribute]
    public class AgeMetadataAttribute : Attribute
    {
        public int Age { get; }

        public AgeMetadataAttribute(int age)
        {
            Age = age;
        }
    }

    /// <summary>
    /// Step 2. Apply Your Metadata Attribute
    /// </summary>
    public interface IArtwork
    {
        void Display();
    }

    [AgeMetadata(100)]
    public class CenturyArtwork : IArtwork
    {
        public void Display()
        {
            Console.WriteLine("CenturyArtwork Displayed");
        }
    }

    [AgeMetadata(25)]
    public class QuarterCenturyArtwork : IArtwork
    {
        public void Display()
        {
            Console.WriteLine("QuarterCenturyArtwork Displayed");
        }
    }

    /// <summary>
    /// Step 3. Use Metadata Filters on Consumers
    /// </summary>
    public interface IDisplay
    {
    }

    public class ArtDisplay : IDisplay
    {
        public ArtDisplay([MetadataFilter("Age", 25)] IArtwork art)
        {
            art.Display();
        }
    }
}