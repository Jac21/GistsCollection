using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Features.Metadata;

namespace AdapterPatternImplementation
{
    public class Program
    {
        private static IContainer Container { get; set; }

        private static void Main()
        {
            var builder = new ContainerBuilder();

            // Register the services to be adapted
            builder
                .RegisterType<SaveCommand>()
                .As<ICommand>()
                .WithMetadata("Name", "Save File");

            builder
                .RegisterType<OpenCommand>()
                .As<ICommand>()
                .WithMetadata("Name", "Open File");

            // Then register the adapter. In this case, the ICommand
            // registrations are using some metadata, so we're
            // adapting Meta<ICommand> instead of plain ICommand.
            builder.RegisterAdapter<Meta<ICommand>, ToolbarButton>(
                cmd => new ToolbarButton(cmd.Value, (string) cmd.Metadata["Name"]));

            builder.RegisterType<EditorWindow>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var buttons = scope.Resolve<IEnumerable<ToolbarButton>>();

                foreach (var button in buttons)
                {
                    button.Click();
                    button.ExposeMetadata();
                }
            }
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("SaveCommand Executed!");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("OpenCommand Executed!");
        }
    }

    public class ToolbarButton
    {
        private readonly ICommand command;
        private readonly string metadata;

        public ToolbarButton(ICommand command, string metadata)
        {
            this.command = command;
            this.metadata = metadata;
        }

        public void Click()
        {
            command.Execute();
        }

        public void ExposeMetadata()
        {
            Console.WriteLine(metadata);
        }
    }

    /// <summary>
    /// Example class that would accept all toolbar buttons as an enumerable dependency
    /// </summary>
    public class EditorWindow
    {
        public EditorWindow(IEnumerable<ToolbarButton> toolbarButtons)
        {
            // ¯\_(ツ)_ /¯
        }
    }
}