using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingKs.Data.Entities;

namespace CountingKs.Data
{
  public class CountingKsContext : DbContext
  {
    public CountingKsContext()
      : base("DefaultConnection")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
    }

    static CountingKsContext()
    {
      Database.SetInitializer(new MigrateDatabaseToLatestVersion<CountingKsContext, CountingKsMigrationConfiguration>());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      CountingKsMapping.Configure(modelBuilder);
    }

    public DbSet<ApiUser> ApiUsers { get; set; }
    public DbSet<AuthToken> AuthTokens { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Measure> Measures { get; set; }
    public DbSet<Diary> Diaries { get; set; }
    public DbSet<DiaryEntry> DiaryEntries { get; set; }
  }
}
