using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class MappingOnSomeNotResolved<T, TResult>: IMapped<T, TResult>
    {

        private T Value { get; }

        public MappingOnSomeNotResolved(T value)
        {
            this.Value = value;
        }

        public IFilteredMapped<T, TResult> When(Func<T, bool> predicate) =>
            predicate(this.Value)
                ? (IFilteredMapped<T, TResult>)new SomeMatchedForMapping<T, TResult>(this.Value)
                : new SomeNotMatchedForMapping<T, TResult>(this.Value);

        public IFilteredMapped<T, TResult> WhenSome() =>
            new SomeMatchedForMapping<T, TResult>(this.Value);

        public IFilteredNoneMapped<T, TResult> WhenNone() =>
            new SomeNotMatchedAsNoneForMapping<T, TResult>(this.Value);

        public TResult Map()
        {
            throw new InvalidOperationException();
        }
    }
}