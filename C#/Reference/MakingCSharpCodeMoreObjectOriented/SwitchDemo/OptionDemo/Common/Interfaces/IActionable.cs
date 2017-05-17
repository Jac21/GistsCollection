using System;

namespace SwitchDemo.Common.Interfaces
{
    public interface IActionable<T>
    {
        IFilteredActionable<T> When(Func<T, bool> predicate);
        IFilteredActionable<T> WhenSome();
        IFilteredNoneActionable<T> WhenNone();
        void Execute();
    }
}