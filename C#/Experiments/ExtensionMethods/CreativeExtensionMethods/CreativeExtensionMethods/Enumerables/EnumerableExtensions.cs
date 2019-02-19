using System;
using System.Collections.Generic;
using System.Linq;

namespace CreativeExtensionMethods.Enumerables
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Simple solution to set something in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEachInList<T>(this IEnumerable<T> collection, Action<T> action)
        {
            var forEach = collection.ToList();
            foreach (T item in forEach)
            {
                action(item);
            }

            return forEach;
        }
    }
}