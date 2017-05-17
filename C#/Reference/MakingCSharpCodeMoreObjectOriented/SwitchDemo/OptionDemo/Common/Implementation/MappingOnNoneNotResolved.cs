using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class MappingOnNoneNotResolved<T, TResult>: IMapped<T, TResult>
    {
        public IFilteredMapped<T, TResult> When(Func<T, bool> predicate) =>
            new NoneNotMatchedForMapping<T, TResult>();

        public IFilteredMapped<T, TResult> WhenSome() =>
            new NoneNotMatchedForMapping<T, TResult>();

        public IFilteredNoneMapped<T, TResult> WhenNone() =>
            new NoneMatchedForMapping<T, TResult>();

        public TResult Map()
        {
            throw new InvalidOperationException();
        }
        
    }
}