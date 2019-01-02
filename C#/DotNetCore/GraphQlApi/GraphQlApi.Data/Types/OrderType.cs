using GraphQL.Types;
using GraphQlApi.Models;
using GraphQlApi.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlApi.Data.Types
{
    /// <summary>
    /// The GraphQL data object related to a <see cref="Order"/>
    /// </summary>
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(ISalesService service)
        {
            Name = "Order";

            Field(order => order.Id)
                .Description("Order Id");
            Field(order => order.ConfirmationNumber)
                .Description("The Order Confirmation Number");
            Field(order => order.CreatedDateUTC, type: typeof(DateGraphType))
                .Description("The UTC date when order was created");

            Field<CustomerType>("customer", resolve: context =>
            {
                try
                {
                    var request = new List<int> { context.Source.CustomerId };
                    return service.GetCustomersAsync(request).Result.First();
                }
                catch
                {
                    return new Customer();
                }
            });

            Field<EmployeeType>("employee",
                resolve: context =>
                {
                    try
                    {
                        var request = new List<int> { context.Source.EmployeeId };
                        return service.GetEmployeesAsync(request).Result.First();
                    }
                    catch
                    {
                        //I would usually add logs here. Skipping that for demo simplicity
                        return new Employee();
                    }
                });

            Field<ListGraphType<OrderDetailType>>("OrderDetails",
                resolve: context =>
                {
                    try
                    {
                        var request = new List<int> { context.Source.Id };
                        return service.GetOrderDetailsAsync(request).Result;
                    }
                    catch
                    {
                        //I would usually add logs here. Skipping that for demo simplicity
                        return new List<OrderDetail>();
                    }
                });
        }
    }
}