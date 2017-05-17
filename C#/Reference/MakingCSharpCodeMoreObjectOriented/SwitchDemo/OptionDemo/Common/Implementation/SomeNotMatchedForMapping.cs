using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class SomeNotMatchedForMapping<T, TResult>: IFilteredMapped<T, TResult>
    {
        private T Value;

        public SomeNotMatchedForMapping(T value)
        {
            this.Value = value;
        }

        public IMapped<T, TResult> MapTo(Func<T, TResult> mapping) =>
            new MappingOnSomeNotResolved<T, TResult>(this.Value);
    }
}