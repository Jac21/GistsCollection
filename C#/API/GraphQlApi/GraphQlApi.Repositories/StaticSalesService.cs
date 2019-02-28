using GraphQlApi.Models;
using GraphQlApi.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlApi.Repositories
{
    public class StaticSalesService : ISalesService
    {
        public async Task<List<Customer>> GetCustomersAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                ? await Task.FromResult(InMemoryData.Customers)
                : await Task.FromResult(InMemoryData.Customers.Where
                    (customer => Ids.Contains(customer.Id)).ToList());
        }

        public async Task<List<Department>> GetDepartmentsAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                ? await Task.FromResult(InMemoryData.Departments)
                : await Task.FromResult(InMemoryData.Departments.Where(department => Ids.Contains(department.Id)).ToList());
        }

        public async Task<List<Employee>> GetEmployeesAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                ? await Task.FromResult(InMemoryData.Employees)
                : await Task.FromResult(InMemoryData.Employees.Where(employee => Ids.Contains(employee.Id)).ToList());
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                ? await Task.FromResult(InMemoryData.OrderDetails)
                : await Task.FromResult(InMemoryData.OrderDetails.Where(detail => Ids.Contains(detail.Id)).ToList());
        }

        public async Task<List<Order>> GetOrdersAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                       ? await Task.FromResult(InMemoryData.Orders)
                       : await Task.FromResult(InMemoryData.Orders.Where(order => Ids.Contains(order.Id)).ToList());
        }

        public async Task<List<ProductCategory>> GetProductCategoriesAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
    ? await Task.FromResult(InMemoryData.ProductCategories)
    : await Task.FromResult(InMemoryData.ProductCategories.Where(category => Ids.Contains(category.Id)).ToList());
        }

        public async Task<List<Product>> GetProductsAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                ? await Task.FromResult(InMemoryData.Products)
                : await Task.FromResult(InMemoryData.Products.Where(product => Ids.Contains(product.Id)).ToList());
        }

        public async Task<List<Provider>> GetProvidersAsync(List<int> Ids = null)
        {
            return Ids == null || !Ids.Any()
                ? await Task.FromResult((InMemoryData.Providers))
                : await Task.FromResult(InMemoryData.Providers.Where(provider => Ids.Contains(provider.Id)).ToList());
        }
    }
}