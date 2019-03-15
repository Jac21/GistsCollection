using DotNetCoreWebApiBestPractices.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApiBestPractices.Contexts
{
    public class OwnerContext : DbContext
    {
        public OwnerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
    }
}