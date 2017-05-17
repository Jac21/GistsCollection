using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class ActionResolved<T>: IActionable<T>, IFilteredActionable<T>, IFilteredNoneActionable<T>
    {
        private Action Action { get; }

        public ActionResolved(Action action)
        {
            this.Action = action;
        }

        public IFilteredActionable<T> When(Func<T, bool> predicate) => this;

        public IFilteredActionable<T> WhenSome() => this;

        public IFilteredNoneActionable<T> WhenNone() => this;

        public IActionable<T> Do(Action<T> action) => this;

        public IActionable<T> Do(Action action) => this;

        public void Execute() => this.Action();

    }
}