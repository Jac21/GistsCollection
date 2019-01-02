using GraphQL.Types;
using GraphQlApi.Models;

namespace GraphQlApi.Data.Types
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Name = "Customer";

            Field(customer => customer.Id).Description("The customer Id");
            Field(customer => customer.FirstName).Description("The customer's first name");
            Field(customer => customer.LastName).Description("The customer's last name");
            Field(customer => customer.LoyaltyPoints).Description("The customer's accumulated loyalty points");
        }
    }
}