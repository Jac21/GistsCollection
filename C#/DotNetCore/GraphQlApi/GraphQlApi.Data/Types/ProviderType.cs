using GraphQL.Types;
using GraphQlApi.Models;

namespace GraphQlApi.Data.Types
{
    /// <summary>
    /// The GraphQL data object related to a <see cref="Provider"/>
    /// </summary>
    public class ProviderType : ObjectGraphType<Provider>
    {
        public ProviderType()
        {
            Name = "Provider";
            Field(provider => provider.Id).Description("Provider Id");
            Field(provider => provider.Name).Description("Provider Name");
        }
    }
}