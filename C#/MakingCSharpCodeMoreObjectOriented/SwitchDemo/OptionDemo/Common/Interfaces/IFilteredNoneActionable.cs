using System;

namespace SwitchDemo.Common.Interfaces
{
    public interface IFilteredNoneActionable<T>
    {
        IActionable<T> Do(Action action);
    }
}