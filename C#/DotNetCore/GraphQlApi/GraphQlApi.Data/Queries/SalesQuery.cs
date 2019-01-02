using GraphQL.Types;
using GraphQlApi.ServiceInterfaces;
using GraphQlApi.Data.Types;

namespace GraphQlApi.Data.Queries
{
    public class SalesQuery : ObjectGraphType<object>
    {
        public SalesQuery(ISalesService service)
        {
            Name = "SalesOrders";

            Field<ListGraphType<OrderType>>("orders",
                resolve: context => service.GetOrdersAsync().Result);
        }
    }
}