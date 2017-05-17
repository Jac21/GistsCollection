using System;
using System.Collections.Generic;

namespace SwitchDemo.Common
{
    static class EnumerableExtensions
    {
        public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T obj in sequence)
                action(obj);
        }
    }
}
