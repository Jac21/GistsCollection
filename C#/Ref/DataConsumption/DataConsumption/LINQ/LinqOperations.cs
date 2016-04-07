using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataConsumption.LINQ
{
    public class LinqOperations
    {
        // class fields
        private readonly int[] _data;

        /// <summary>
        /// constructor, initialize data array
        /// </summary>
        public LinqOperations()
        {
            _data = new[] {1, 2, 5, 6, 11};
        }

        /// <summary>
        /// LINQ Where Operator example
        /// </summary>
        public void WhereOperator()
        {
            var result = _data.Where(d => d%2 == 0);

            foreach (var i in result)
            {
                Console.WriteLine("LINQ Select: {0}", i);
            }
        }

        /// <summary>
        /// LINQ OrderBy Operator example
        /// </summary>
        public void OrderByOperator()
        {
            var result = _data.Where(d => d > 5).OrderByDescending(d => d);

            foreach (var i in result)
            {
                Console.WriteLine("LINQ Order By: {0}", i);
            }
        }

        /// <summary>
        /// LINQ to XML operation example
        /// </summary>
        /// <param name="xml"></param>
        public void LinqToXml(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            IEnumerable<string> personNames = from p in doc.Descendants("person")
                select (string) p.Attribute("firstname") + " " + (string) p.Attribute("lastname");

            foreach (string s in personNames)
            {
                Console.WriteLine("LINQ to XML: {0}", s);
            }
        }
    }


    /*
     * Implementations
     */

    public static class LinqExtensions
    {
        // implementing Where
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
