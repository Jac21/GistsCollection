
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using CountingKs.Data.Entities;

namespace CountingKs.Data
{
  public class CountingKsMigrationConfiguration : DbMigrationsConfiguration<CountingKsContext>
  {
    public CountingKsMigrationConfiguration()
    {
      this.AutomaticMigrationsEnabled = true;
      this.AutomaticMigrationDataLossAllowed = true;
    }

#if DEBUG
    protected override void Seed(CountingKsContext context)
    {
      // Seed the database if necessary
      new CountingKsSeeder(context).Seed();
    }
#endif
  }
}
