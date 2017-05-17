using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingKs.Data
{
  public static class Extensions
  {
    public static void AttachAsModified<T>(this DbSet<T> dbSet, T entity, DbContext ctx) where T : class
    {
      DbEntityEntry<T> entityEntry = ctx.Entry(entity);
      if (entityEntry.State == EntityState.Detached)
      {
        // attach the entity
        dbSet.Attach(entity);
      }

      // transition the entity to the modified state
      entityEntry.State = EntityState.Modified;
    }

  }
}
