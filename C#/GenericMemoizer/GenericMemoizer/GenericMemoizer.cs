using System;
using System.Collections.Generic;

namespace GenericMemoizer
{
    /// <summary>
    /// Overloaded batch of methods that implement generic memoization
    /// techniques, ranging from no input to two
    /// </summary>
    public class GenericMemoizer
    {
        /// <summary>
        /// Caches only one value
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<TReturn> MemoizerFunc<TReturn>(Func<TReturn> func)
        {
            object cache = null;
            return () =>
            {
                if (cache == null)
                {
                    cache = func();
                }
                return (TReturn)cache;
            };
        }

        /// <summary>
        /// Only one input value, can use the input object
        /// itself as the key into the dictionary cache
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<TSource, TReturn> MemoizerFunc<TSource, TReturn>(Func<TSource, TReturn> func)
        {
            var cache = new Dictionary<TSource, TReturn>();
            return s =>
            {
                if (!cache.ContainsKey(s))
                {
                    cache[s] = func(s);
                }
                return cache[s];
            };
        }

        /// <summary>
        /// With more than one input, have to compute using the concatenation
        /// of object hash codes
        /// TODO: Try using System.Tuple
        /// </summary>
        /// <typeparam name="TSource1"></typeparam>
        /// <typeparam name="TSource2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<TSource1, TSource2, TReturn> MemoizerFunc<TSource1, TSource2, TReturn>(
            Func<TSource1, TSource2, TReturn> func)
        {
            var cache = new Dictionary<string, TReturn>();
            return (s1, s2) =>
            {
                var key = s1.GetHashCode().ToString() + s2.GetHashCode().ToString();
                if (!cache.ContainsKey(key))
                {
                    cache[key] = func(s1, s2);
                }
                return cache[key];
            };
        }

        /// <summary>
        /// Basic memoization example involving dictionary caching and string formatting
        /// </summary>
        public class BasicMemoizer
        {
            // class field
            static Dictionary<string, string> _lowercase = new Dictionary<string, string>();

            public static string DictionaryMemoizer(string value)
            {
                string lookup;
                if (_lowercase.TryGetValue(value, out lookup))
                {
                    return lookup;
                }

                lookup = value.ToLower();

                _lowercase[value] = lookup;
                return lookup;
            }
        }

        static void Main()
        {
            // Execution example
            Func<string, string> singleInput = MemoizerFunc(
                (string input) => String.Format("Hello, {0}!", input));

            // output: "Hello, World!"
            Console.WriteLine(singleInput.Invoke("World"));

            // Another
            Func<string, string, string> doubleInput = MemoizerFunc(
                (string input1, string input2) => String.Format("Hi {0} and {1}!", input1, input2));

            Console.WriteLine(doubleInput.Invoke("One", "Two"));
        }
    }
}
