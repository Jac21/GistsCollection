using System;
using System.Collections.Generic;
using MyFluentInterface.Conditions;
using MyFluentInterface.Entites;
using MyFluentInterface.Utilities;

namespace MyFluentInterface.FluentBuilders
{
    public class FluentBuilderFns : ICanAddCondition, ICanAddWhereValue, ICanAddPartsOrWhereOrBuild
    {
        private Func<Product, Product> fn;

        public string ProductModelName;
        public Dictionary<object, bool> WhereConditions;

        #region Initiating Method(s)

        public FluentBuilderFns Begin()
        {
            fn = ignored => new Product();
            WhereConditions = new Dictionary<object, bool>();
            return this;
        }

        #endregion

        #region Chaining Method(s)

        public ICanAddPartsOrWhereOrBuild AddEngine()
        {
            fn = FnUtils.Compose(fn, product =>
            {
                product.Parts.Add("Engine");
                return product;
            });

            return this;
        }

        public ICanAddPartsOrWhereOrBuild AddSteeringWheel()
        {
            fn = FnUtils.Compose(fn, product =>
            {
                product.Parts.Add("Steering Wheel");
                return product;
            });

            return this;
        }

        public ICanAddPartsOrWhereOrBuild AddTires(int numberOfTires)
        {
            fn = FnUtils.Compose(fn, product =>
            {
                for (int i = 0; i < numberOfTires; i++)
                {
                    product.Parts.Add("Tire");
                }

                return product;
            });

            return this;
        }

        public ICanAddWhereValue Where(string modelName)
        {
            ProductModelName = modelName;

            return this;
        }

        public ICanAddPartsOrWhereOrBuild IsEqualTo(object value)
        {
            WhereConditions.Add(value, true);

            return this;
        }

        public ICanAddPartsOrWhereOrBuild IsNotEqualTo(object value)
        {
            WhereConditions.Add(value, false);

            return this;
        }

        #endregion

        #region Executing Method(s)

        public Product Build()
        {
            return fn(null);
        }

        #endregion
    }
}