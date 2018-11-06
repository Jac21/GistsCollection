using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using MyExpressionTree.Models;

namespace MyExpressionTree
{
    public static class ExpressionTree
    {
        /// <summary>
        /// To represent x => x.LastName == "Curry" in expression trees, we have to write the following method
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Func<User, bool> GetDynamicQueryWithExpressionTrees(string propertyName, string value)
        {
            // x =>
            var param = Expression.Parameter(typeof(User), "x");
            var member = Expression.Property(param, propertyName);

            // value ("Curry")
            UnaryExpression valueExpression = GetValueExpression(propertyName, value, param);

            // x.LastName == "Curry"
            Expression body = Expression.Equal(member, valueExpression);

            // x => x.LastName == "Curry"
            var final = Expression.Lambda<Func<User, bool>>(body: body, parameters: param);

            // compiles the expression tree to a func delegate
            return final.Compile();
        }

        /// <summary>
        /// Convert our value to the appropriate type.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private static UnaryExpression GetValueExpression(string propertyName, string val, ParameterExpression param)
        {
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo) member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();

            // will give the integer value if the string is integer
            var propertyValue = converter.ConvertFromInvariantString(val);
            var constant = Expression.Constant(propertyValue);
            return Expression.Convert(constant, propertyType);
        }
    }
}