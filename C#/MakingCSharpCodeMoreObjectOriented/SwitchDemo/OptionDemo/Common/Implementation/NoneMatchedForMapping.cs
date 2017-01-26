using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class NoneMatchedForMapping<T, TResult>: IFilteredNoneMapped<T, TResult>
    {
        public IMapped<T, TResult> MapTo(Func<TResult> mapping) =>
            new MappingResolved<T, TResult>(mapping());
    }
}