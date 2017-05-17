using System;
using SwitchDemo.Common;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo
{
    class Program
    {

        static void Main(string[] args)
        {

            IOption<string> name = Option<string>.Some("something");

            name
                .When(s => s.Length > 3).Do(s => Console.WriteLine($"{s} long"))
                .WhenSome().Do(s => Console.WriteLine($"{s} short"))
                .WhenNone().Do(() => Console.WriteLine("missing"))
                .Execute();

            int length =
                name
                    .When(s => s.Length > 3).MapTo(s => s.Length)
                    .WhenSome().MapTo(s => 3)
                    .WhenNone().MapTo(() => 0)
                    .Map();

            Console.ReadLine();

        }
    }
}
