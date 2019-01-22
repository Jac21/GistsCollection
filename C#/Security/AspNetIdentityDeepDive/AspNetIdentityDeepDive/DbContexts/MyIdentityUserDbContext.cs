using AspNetIdentityDeepDive.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetIdentityDeepDive.DbContexts
{
    public class MyIdentityUserDbContext : IdentityDbContext<MyIdentityUser>
    {
        public MyIdentityUserDbContext(DbContextOptions<MyIdentityUserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MyIdentityUser>(user => user.HasIndex(x => x.Locale).IsUnique(false));
            builder.Entity<Organization>(org =>
            {
                org.ToTable("Organization");
                org.HasKey(x => x.Id);

                org.HasMany<MyIdentityUser>().WithOne().HasForeignKey(x => x.OrgId).IsRequired(false);
            });
        }
    }
}