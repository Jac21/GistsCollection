using GraphQL;
using GraphQL.Types;
using GraphQlApi.Data.Queries;

namespace GraphQlApi.Data.Schemas
{
    public class SalesSchema : Schema
    {
        public SalesSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<SalesQuery>();
        }
    }
}