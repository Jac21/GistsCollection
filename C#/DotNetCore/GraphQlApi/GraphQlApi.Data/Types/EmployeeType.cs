using GraphQL.Types;
using GraphQlApi.Models;
using GraphQlApi.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlApi.Data.Types
{
    /// <summary>
    /// The GraphQL data object related to a <see cref="Employee"/>
    /// </summary>
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType(ISalesService service)
        {
            Name = "Employee";

            Field(employee => employee.Id).Description("Employee Id");
            Field(employee => employee.FirstName).Description("Employee First Name");
            Field(employee => employee.LastName).Description("Employee Last Name");
            Field(employee => employee.Title).Description("Employee Title");
            Field(employee => employee.HiredDate, type: typeof(DateGraphType)).Description("When the employee was hired");

            Field<DepartmentType>("department",
                resolve: context =>
                {
                    try
                    {
                        var request = new List<int> { context.Source.DepartmentId };
                        return service.GetDepartmentsAsync(request).Result.First();
                    }
                    catch
                    {
                        //I would usually add logs here. Skipping that for demo simplicity
                        return new Department();
                    }
                });
        }
    }
}