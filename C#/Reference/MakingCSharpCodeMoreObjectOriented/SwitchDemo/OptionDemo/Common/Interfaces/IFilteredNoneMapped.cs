using System;

namespace SwitchDemo.Common.Interfaces
{
    public interface IFilteredNoneMapped<T, TResult>
    {
        IMapped<T, TResult> MapTo(Func<TResult> mapping);
    }
}