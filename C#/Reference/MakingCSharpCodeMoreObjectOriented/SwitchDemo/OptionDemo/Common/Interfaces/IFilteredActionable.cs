using System;

namespace SwitchDemo.Common.Interfaces
{
    public interface IFilteredActionable<T>
    {
        IActionable<T> Do(Action<T> action);
    }
}