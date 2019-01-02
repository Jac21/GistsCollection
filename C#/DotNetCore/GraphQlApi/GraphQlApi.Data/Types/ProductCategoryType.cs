using GraphQL.Types;
using GraphQlApi.Models;

namespace GraphQlApi.Data.Types
{
    /// <summary>
    /// The GraphQL data object related to a <see cref="ProductCategory"/>
    /// </summary>
    public class ProductCategoryType : ObjectGraphType<ProductCategory>
    {
        public ProductCategoryType()
        {
            Name = "ProductCategory";

            Field(category => category.Id).Description("Product Category Id");
            Field(categgory => categgory.Name).Description("Product Category Name");
        }
    }
}