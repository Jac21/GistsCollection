namespace APM.WebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<APM.WebApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(APM.WebApi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Products.AddOrUpdate(
                new WebAPI.Models.Product {
                    Description = "Leaf rake with 48-inch wooden handle.",
                    Price = 1995,
                    ProductCode = "GDN-0011",
                    ProductId = 1,
                    ProductName = "Leaf Rake",
                    ReleaseDate = DateTime.Now
                  }
            );
            context.Products.AddOrUpdate(
                new WebAPI.Models.Product {
                    Description = "15 gallon capacity rolling garden cart.",
                    Price = 1995,
                    ProductCode = "GDN-0011",
                    ProductId = 2,
                    ProductName = "Rolling Garden Cart",
                    ReleaseDate = DateTime.Now
                  }
            );
            context.Products.AddOrUpdate(
                new WebAPI.Models.Product {
                    Description = "Ergonomic firm-grim hand cultivator.",
                    Price = 1995,
                    ProductCode = "GDN-0011",
                    ProductId = 3,
                    ProductName = "Cultivator",
                    ReleaseDate = DateTime.Now
                  }
            );
            context.Products.AddOrUpdate(
                new WebAPI.Models.Product {
                    Description = "Firm grip 14-inch hand trowel.",
                    Price = 1995,
                    ProductCode = "GDN-0011",
                    ProductId = 4,
                    ProductName = "Hand Trowel",
                    ReleaseDate = DateTime.Now
                  }
            );
            context.Products.AddOrUpdate(
                new WebAPI.Models.Product {
                    Description = "Curved claw steel hammer.",
                    Price = 1995,
                    ProductCode = "GDN-0011",
                    ProductId = 5,
                    ProductName = "Steel Hammer",
                    ReleaseDate = DateTime.Now
                  }
            );
        }
    }
}
