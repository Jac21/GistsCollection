using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class SomeMatched<T>: IFiltered<T>
    {
        private T Value { get; }

        public SomeMatched(T value)
        {
            this.Value = value;
        }

        public IActionable<T> Do(Action<T> action) =>
            new ActionResolved<T>(() => action(this.Value));

        public IMapped<T, TResult> MapTo<TResult>(Func<T, TResult> mapping) =>
            new MappingResolved<T, TResult>(mapping(this.Value));
    }
}