using GraphQL.Types;
using GraphQlApi.Models;
using GraphQlApi.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlApi.Data.Types
{
    /// <summary>
    /// The GraphQL data object related to a <see cref="Product"/>
    /// </summary>
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ISalesService service)
        {
            Name = "Product";

            Field(product => product.Id).Description("Product Id");
            Field(product => product.Name).Description("Product Name");
            Field(product => product.Code).Description("Product Code, abbreviation or Serial Number");

            Field(product => product.ProductCategoryId).Description("Product Name");

            Field<ProviderType>("provider",
                resolve: context =>
                {
                    try
                    {
                        var request = new List<int> { context.Source.ProviderId };
                        return service.GetProvidersAsync(request).Result.First();
                    }
                    catch
                    {
                        //I would usually add logs here. Skipping that for demo simplicity
                        return new Provider();
                    }
                });

            Field<ProductCategoryType>("category",
                resolve: context =>
                {
                    try
                    {
                        var request = new List<int> { context.Source.ProductCategoryId };
                        return service.GetProductCategoriesAsync(request).Result.First();
                    }
                    catch
                    {
                        //I would usually add logs here. Skipping that for demo simplicity
                        return new ProductCategory();
                    }
                });
        }
    }
}