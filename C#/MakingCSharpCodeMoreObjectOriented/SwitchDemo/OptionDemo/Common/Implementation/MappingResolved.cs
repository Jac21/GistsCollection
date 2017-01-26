using System;
using SwitchDemo.Common.Interfaces;

namespace SwitchDemo.Common.Implementation
{
    internal class MappingResolved<T, TResult>: IMapped<T, TResult>, IFilteredMapped<T, TResult>, IFilteredNoneMapped<T, TResult>
    {

        private TResult Result { get; }

        public MappingResolved(TResult result)
        {
            this.Result = result;
        }

        public IFilteredMapped<T, TResult> When(Func<T, bool> predicate) => this;

        public IFilteredMapped<T, TResult> WhenSome() => this;

        public IFilteredNoneMapped<T, TResult> WhenNone() => this;

        public IMapped<T, TResult> MapTo(Func<T, TResult> mapping) => this;

        public IMapped<T, TResult> MapTo(Func<TResult> mapping) => this;

        public TResult Map() => this.Result;

    }
}