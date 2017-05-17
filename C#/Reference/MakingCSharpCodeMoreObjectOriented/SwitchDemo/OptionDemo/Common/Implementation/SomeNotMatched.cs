using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class SomeNotMatched<T>: IFiltered<T>, IFilteredNoneActionable<T>
    {
        private T Value { get; }

        public SomeNotMatched(T value)
        {
            this.Value = value;
        }

        public IActionable<T> Do(Action<T> action) =>
            new ActionOnSomeNotResolved<T>(this.Value);

        public IActionable<T> Do(Action action) =>
            new ActionOnSomeNotResolved<T>(this.Value);

        public IMapped<T, TResult> MapTo<TResult>(Func<T, TResult> mapping) =>
            new MappingOnSomeNotResolved<T, TResult>(this.Value);

    }
}