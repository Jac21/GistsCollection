using GraphQlApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQlApi.ServiceInterfaces
{
    public interface ISalesService
    {
        /// <summary>
        /// Gets a list of <see cref="Employee"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(Optional) The list of employee Ids to search and retrieve</param>
        /// <returns>When found, it returns a list of <see cref="Employee"/></returns>
        Task<List<Employee>> GetEmployeesAsync(List<int> Ids = null);

        /// <summary>
        /// Gets a list of <see cref="Customer"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of customer Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="Customer"/></returns>
        Task<List<Customer>> GetCustomersAsync(List<int> Ids = null);


        /// <summary>
        /// Gets a list of <see cref="Product"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of product Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="Product"/></returns>
        Task<List<Product>> GetProductsAsync(List<int> Ids = null);

        /// <summary>
        /// Gets a list of <see cref="Provider"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of provider Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="Provider"/></returns>
        Task<List<Provider>> GetProvidersAsync(List<int> Ids = null);

        /// <summary>
        /// Gets a list of <see cref="Department"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of department Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="Department"/></returns>
        Task<List<Department>> GetDepartmentsAsync(List<int> Ids = null);

        /// <summary>
        /// Gets a list of <see cref="ProductCategory"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of product category Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="ProductCategory"/></returns>
        Task<List<ProductCategory>> GetProductCategoriesAsync(List<int> Ids = null);

        /// <summary>
        /// Gets a list of <see cref="Order"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of sales order Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="Order"/></returns>
        Task<List<Order>> GetOrdersAsync(List<int> Ids = null);

        /// <summary>
        /// Gets a list of <see cref="OrderDetail"/> asynchronously based on the given parameters
        /// </summary>
        /// <param name="Ids">(optional) The list of order detail Ids to search and retrieve</param>
        /// <returns>When found, returns the list of <see cref="OrderDetail"/></returns>
        Task<List<OrderDetail>> GetOrderDetailsAsync(List<int> Ids = null);
    }
}