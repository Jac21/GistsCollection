using GraphQL.Types;
using GraphQlApi.Models;
using GraphQlApi.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlApi.Data.Types
{
    public class OrderDetailType : ObjectGraphType<OrderDetail>
    {
        public OrderDetailType(ISalesService service)
        {
            Name = "Sales";

            Field(detail => detail.Id).Description("Order Detail Id");
            Field(detail => detail.OrderId).Description("Order Id");
            Field(detail => detail.Quantity).Description("Order Quantity");
            Field(detail => detail.Price).Description("Product Sale Price");

            Field<ProductType>("product",
                resolve: context =>
                {
                    try
                    {
                        var request = new List<int> { context.Source.ProductId };
                        return service.GetProductsAsync(request).Result.First();
                    }
                    catch
                    {
                        //I would usually add logs here. Skipping that for demo simplicity
                        return new Product();
                    }
                });
        }
    }
}