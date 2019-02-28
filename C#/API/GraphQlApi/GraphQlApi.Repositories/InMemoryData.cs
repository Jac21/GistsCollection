using GraphQlApi.Models;
using System;
using System.Collections.Generic;

namespace GraphQlApi.Repositories
{/// <summary>
 /// Returns static in memory data for different entities related to ISalesService
 /// </summary>
    public static class InMemoryData
    {
        /// <summary>
        /// List of departments
        /// </summary>
        public static List<Department> Departments = new List<Department>
        {
            new Department
            {
                Id = 1,
                Name = "Electronics"
            },
            new Department
            {
                Id = 2,
                Name = "Games"
            }
        };

        /// <summary>
        /// List of employees
        /// </summary>
        public static List<Employee> Employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DepartmentId = 1, //electronics department
                HiredDate = DateTime.UtcNow.AddYears(-1),
                Title = "Junior Sales Representative"
            },
            new Employee
            {
                Id = 2,
                FirstName = "Henry",
                LastName = "Smith",
                DepartmentId = 2, //games department
                HiredDate = DateTime.UtcNow.AddYears(-1),
                Title = "Senior Sales Representative"
            }
        };

        /// <summary>
        /// List of customers
        /// </summary>
        public static List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                FirstName = "Gilberto",
                LastName = "Salguero",
                LoyaltyPoints = 100
            },
            new Customer
            {
                Id = 2,
                FirstName = "Nahin",
                LastName = "Gaitan",
                LoyaltyPoints = 56
            },
            new Customer
            {
                Id = 3,
                FirstName = "Mario",
                LastName = "Robles",
                LoyaltyPoints = 200
            }
        };

        public static List<Provider> Providers = new List<Provider>
        {
            new Provider
            {
                Id = 1,
                Name = "EA Sports"
            },
            new Provider
            {
                Id = 2,
                Name = "Sony"
            }
        };

        public static List<ProductCategory> ProductCategories = new List<ProductCategory>
        {
            new ProductCategory
            {
                Id = 1,
                Name = "Electronics"
            },
            new ProductCategory
            {
                Id = 2,
                Name = "Games",
                ParentCategoryId = 1
            }
        };

        /// <summary>
        /// List of available products
        /// </summary>
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Code = "AAA-1000",
                Name = "FIFA 2018 - PS4",
                ProductCategoryId = 2, //Games
                ProviderId = 1 // EA Sports
            },
            new Product
            {
                Id = 2,
                Code = "BBB-2200",
                Name = "65 inches 4K HD TV Sony",
                ProductCategoryId = 1, //Electronics
                ProviderId = 2 // Sony
            }
        };

        public static List<Order> Orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                ConfirmationNumber = "1001",
                CustomerId = 1, // Gilberto
                EmployeeId = 1, // John
                CreatedDateUTC = DateTime.UtcNow.AddDays(-10)
            },
            new Order
            {
                Id = 2,
                ConfirmationNumber = "1002",
                CustomerId = 2, // Nahin
                EmployeeId = 2, // Henry
                CreatedDateUTC = DateTime.UtcNow.AddDays(-5)
            },
            new Order
            {
                Id = 3,
                ConfirmationNumber = "1003",
                CustomerId = 3, // Mario
                EmployeeId = 2, // Henry
                CreatedDateUTC = DateTime.UtcNow.AddDays(-2)
            }
        };

        public static List<OrderDetail> OrderDetails = new List<OrderDetail>
        {
            new OrderDetail
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,//FIFA Game
                Price = 70,
                Quantity = 1
            },
            new OrderDetail
            {
                Id = 2,
                OrderId = 2,
                ProductId = 2,//TV
                Price = 2000,
                Quantity = 1
            },
            new OrderDetail
            {
                Id = 3,
                OrderId = 3,
                ProductId = 2,//Game
                Price = 65,
                Quantity = 1
            }
        };
    }
}