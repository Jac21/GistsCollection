using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CountingKs.Data.Entities;

namespace CountingKs.Data
{
  public class CountingKsMapping
  {
    public static void Configure(DbModelBuilder modelBuilder)
    {
      MapFood(modelBuilder);
      MapMeasure(modelBuilder);
      MapDiaryEntry(modelBuilder);
      MapDiary(modelBuilder);
      MapApiUser(modelBuilder);
      MapApiToken(modelBuilder);
    }

    static void MapApiToken(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AuthToken>().ToTable("AuthToken", "Security");
    }

    static void MapApiUser(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ApiUser>().ToTable("ApiUser", "Security");
    }

    static void MapFood(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Food>().ToTable("Food", "Nutrition");
    }

    static void MapMeasure(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Measure>().ToTable("Measure", "Nutrition");
    }

    static void MapDiaryEntry(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<DiaryEntry>().ToTable("DiaryEntry", "FoodDiaries");
    }

    static void MapDiary(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Diary>().ToTable("Diary", "FoodDiaries");
    }

  }
}
