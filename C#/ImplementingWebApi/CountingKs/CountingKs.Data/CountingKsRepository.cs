using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingKs.Data.Entities;

namespace CountingKs.Data
{
  public class CountingKsRepository : ICountingKsRepository
  {
    private CountingKsContext _ctx;
    public CountingKsRepository(CountingKsContext ctx)
    {
      _ctx = ctx;
    }

    public IQueryable<Food> GetAllFoods()
    {
      return _ctx.Foods;
    }

    public IQueryable<Food> GetAllFoodsWithMeasures()
    {
      return _ctx.Foods.Include("Measures");
    }

    public IQueryable<Food> FindFoodsWithMeasures(string searchString)
    {
      // Dumb search but adequate for our tests
      return _ctx.Foods.Include("Measures").Where(f => f.Description.Contains(searchString));
    }


    public IQueryable<Measure> GetMeasuresForFood(int foodId)
    {
      return _ctx.Measures.Include("Food")
                          .Where(m => m.Food.Id == foodId);
    }

    public Food GetFood(int id)
    {
      return _ctx.Foods.Include("Measures").Where(f => f.Id == id).FirstOrDefault();
    }

    public Measure GetMeasure(int id)
    {
      return _ctx.Measures.Include("Food")
                          .Where(m => m.Id == id)
                          .FirstOrDefault();
    }

    public IQueryable<Diary> GetDiaries(string userName)
    {
      return _ctx.Diaries.Include("Entries.FoodItem")
                         .Include("Entries.Measure")
                         .OrderByDescending(d => d.CurrentDate)
                         .Where(d => d.UserName == userName);
    }

    public Diary GetDiary(string userName, DateTime day)
    {
      return GetDiaries(userName).Where(d => d.CurrentDate == day.Date).FirstOrDefault();
    }

    public IQueryable<DiaryEntry> GetDiaryEntries(string userName, DateTime diaryDay)
    {
      return _ctx.DiaryEntries.Include("FoodItem")
                              .Include("Measure")
                              .Include("Diary")
                              .Where(f => f.Diary.UserName == userName && 
                                          f.Diary.CurrentDate == diaryDay);
    }

    public DiaryEntry GetDiaryEntry(string userName, DateTime diaryDay, int id)
    {
      return _ctx.DiaryEntries.Include("FoodItem")
                              .Include("Measure")
                              .Include("Diary")
                              .Where(f => f.Diary.UserName == userName &&
                                          f.Diary.CurrentDate == diaryDay &&
                                          f.Id == id)
                              .FirstOrDefault();
    }

    public IQueryable<ApiUser> GetApiUsers()
    {
      return _ctx.ApiUsers;
    }

    public AuthToken GetAuthToken(string token)
    {
      return _ctx.AuthTokens.Include("ApiUser").Where(t => t.Token == token).FirstOrDefault();
    }

    public bool SaveAll()
    {
      return _ctx.SaveChanges() > 0;
    }

    public bool Insert(AuthToken token)
    {
      try
      {
        _ctx.AuthTokens.Add(token);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool Insert(DiaryEntry entry)
    {
      try
      {
        _ctx.DiaryEntries.Add(entry);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool Insert(Diary diary)
    {
      try
      {
        _ctx.Diaries.Add(diary);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool Update(DiaryEntry entry) 
    {
      return UpdateEntity(_ctx.DiaryEntries, entry);
    }

    public bool Update(Diary diary)
    {
      return UpdateEntity(_ctx.Diaries, diary);
    }

    public bool DeleteDiaryEntry(int id) 
    {
      try
      {
        var entity = _ctx.DiaryEntries.Where(f => f.Id == id).FirstOrDefault();
        if (entity != null) 
        {
          _ctx.DiaryEntries.Remove(entity);
          return true;
        }
      }
      catch
      {
        // TODO Logging
      }

      return false;
    }

    public bool DeleteDiary(int id)
    {
      try
      {
        var entity = _ctx.Diaries.Where(d => d.Id == id).FirstOrDefault();
        if (entity != null)
        {
          _ctx.Diaries.Remove(entity);
          return true;
        }
      }
      catch
      {
        // TODO Logging
      }

      return false;
    }

    // Helper to update objects in context
    bool UpdateEntity<T>(DbSet<T> dbSet, T entity) where T : class
    {
      try
      {
        dbSet.AttachAsModified(entity, _ctx);
        return true;
      }
      catch
      {
        return false;
      }
    }

  }
}
