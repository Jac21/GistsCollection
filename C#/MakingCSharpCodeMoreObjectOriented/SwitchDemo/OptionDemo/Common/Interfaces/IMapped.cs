using System;

namespace SwitchDemo.Common.Interfaces
{
    public interface IMapped<T, TResult>
    {
        IFilteredMapped<T, TResult> When(Func<T, bool> predicate);
        IFilteredMapped<T, TResult> WhenSome();
        IFilteredNoneMapped<T, TResult> WhenNone();
        TResult Map();
    }
}