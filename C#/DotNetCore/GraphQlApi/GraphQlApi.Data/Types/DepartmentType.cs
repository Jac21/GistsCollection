using GraphQL.Types;
using GraphQlApi.Models;

namespace GraphQlApi.Data.Types
{
    /// <summary>
    /// The GraphQL data object related to a <see cref="Department"/>
    /// </summary>
    public class DepartmentType : ObjectGraphType<Department>
    {
        public DepartmentType()
        {
            Name = "Department";

            Field(department => department.Id).Description("Department Id");
            Field(department => department.Name).Description("Department Name");
        }
    }
}