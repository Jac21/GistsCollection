using System;

namespace SwitchDemo.Common.Interfaces
{
    public interface IFilteredMapped<T, TResult>
    {
        IMapped<T, TResult> MapTo(Func<T, TResult> mapping);
    }
}