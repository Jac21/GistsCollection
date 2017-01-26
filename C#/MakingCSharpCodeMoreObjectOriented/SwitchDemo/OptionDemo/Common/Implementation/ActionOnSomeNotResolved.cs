using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class ActionOnSomeNotResolved<T>: IActionable<T>
    {

        private T Value { get; }

        public ActionOnSomeNotResolved(T value)
        {
            this.Value = value;
        }

        public IFilteredActionable<T> When(Func<T, bool> predicate) =>
            predicate(this.Value) ? (IFilteredActionable<T>)new SomeMatched<T>(this.Value) : new SomeNotMatched<T>(this.Value);

        public IFilteredActionable<T> WhenSome() =>
            new SomeMatched<T>(this.Value);

        public IFilteredNoneActionable<T> WhenNone() =>
            new SomeNotMatched<T>(this.Value);

        public void Execute() { }

    }
}