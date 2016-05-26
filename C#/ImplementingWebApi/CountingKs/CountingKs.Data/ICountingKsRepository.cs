using System;
using System.Collections.Generic;
using System.Linq;
using CountingKs.Data.Entities;
namespace CountingKs.Data
{
  public interface ICountingKsRepository
  {
    // General
    bool SaveAll();

    // Food
    IQueryable<Food> FindFoodsWithMeasures(string searchString);
    IQueryable<Food> GetAllFoods();
    IQueryable<Food> GetAllFoodsWithMeasures();
    Food GetFood(int id);
    Measure GetMeasure(int id);

    // Measure
    IQueryable<Measure> GetMeasuresForFood(int foodId);

    // Diary
    IQueryable<Diary> GetDiaries(string userName);
    Diary GetDiary(string userName, DateTime day);

    // DiaryEntry
    IQueryable<DiaryEntry> GetDiaryEntries(string userName, DateTime diaryDay);
    DiaryEntry GetDiaryEntry(string userName, DateTime diaryDay, int id);

    // Users
    IQueryable<ApiUser> GetApiUsers();

    // Tokens
    AuthToken GetAuthToken(string token);

    // Inserts
    bool Insert(DiaryEntry entry);
    bool Insert(Diary diary);
    bool Insert(AuthToken token);

    // Updates
    bool Update(DiaryEntry entry);
    bool Update(Diary diary);

    // Deletes
    bool DeleteDiaryEntry(int id);
    bool DeleteDiary(int id);
  }
}
