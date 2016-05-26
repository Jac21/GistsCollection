//#define TEST_SEED
//#define FORCE_RECREATE

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using CountingKs.Data.Entities;

namespace CountingKs.Data
{
  public class CountingKsSeeder
  {
    CountingKsContext _ctx;
    public CountingKsSeeder(CountingKsContext ctx)
    {
      _ctx = ctx;
    }

    public void Seed()
    {
#if !(TEST_SEED || FORCE_RECREATE)
      if (_ctx.Foods.Count() > 0)
      {
        return;
      }
#endif

#if TEST_SEED || FORCE_RECREATE
      ExecuteQueries(
        "DELETE FROM FoodDiaries.DiaryEntry",
        "DELETE FROM FoodDiaries.Diary",
        "DELETE FROM Nutrition.Measure",
        "DELETE FROM Nutrition.Food",
        "DELETE FROM [Security].[User]"
        "DELETE FROM [Security].[ApiUser]"
      );
#endif

      SeedApiUsers();
      SeedFoods();
      SeedDiaries();

    }

    void SeedApiUsers()
    {
      try
      {
        var user = new ApiUser()
        {
          Name = "My Cool App",
          AppId = "SSB3aWxsIG1ha2UgbXkgQVBJIHNlY3VyZQ==",
          Secret = "VGhpcyBDb3Vyc2UgSXMgQXdlc29tZQ=="
        };

        _ctx.ApiUsers.Add(user);

        var token = new AuthToken()
        {
          Token = "1234567890",
          Expiration = DateTime.Today.AddDays(365),
          ApiUser = user
        };

        _ctx.AuthTokens.Add(token);

        _ctx.SaveChanges();

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    void SeedFoods()
    {

      try
      {
        // Load the excel sheet with data into the database
        var rawData = OpenExcel("~/App_Data/ABBREV.xls", "ABBREV22");

#if TEST_SEED
        var rows = rawData.Rows.Cast<DataRow>().Take(10);
#else
        var rows = rawData.Rows.Cast<DataRow>();
#endif

        foreach (var row in rows)
        {
          var measures = new List<Measure>();
          var food = new Food()
          {
            Description = ToTitleCase(GetData<string>(row, "Shrt_Desc")),
            Measures = measures
          };

          var measureDescription1 = GetData<string>(row, "GmWt_Desc1");
          var measureDescription2 = GetData<string>(row, "GmWt_Desc2");

          if (!string.IsNullOrWhiteSpace(measureDescription1))
          {
            var divisor = GetData<double>(row, "GmWt_1");
            var m = CreateRawMeasure(row, divisor, ToTitleCase(measureDescription1));
            measures.Add(m);
          }

          if (!string.IsNullOrWhiteSpace(measureDescription2))
          {
            var divisor = GetData<double>(row, "GmWt_2");
            var m = CreateRawMeasure(row, divisor, ToTitleCase(measureDescription2));
            measures.Add(m);
          }

          if (measures.Count == 0)
          {
            var m = CreateRawMeasure(row, 100.0, "100g");
            measures.Add(m);
          }

          _ctx.Foods.Add(food);
        }

        _ctx.SaveChanges();
      }
      catch (Exception ex)
      {
        // log
        throw ex;
      }
    }

    void SeedDiaries()
    {


      try
      {
        var diary = new Diary()
        {
          CurrentDate = DateTime.Today,
          UserName = "shawnwildermuth",
        };

        foreach (var food in _ctx.Foods.Take(1000).ToList().OrderBy(m => Guid.NewGuid()).Take(15))
        {
          var entry = new DiaryEntry()
          {
            Diary = diary,
            Quantity = 1.5,
          };

          entry.FoodItem = food;
          entry.Measure = food.Measures.First();

          diary.Entries.Add(entry);
        }

        _ctx.Diaries.Add(diary);

        _ctx.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }



    void ExecuteQueries(params string[] sqlStatements)
    {
      foreach (var sql in sqlStatements)
      {
        _ctx.Database.ExecuteSqlCommand(sql);
      }

    }

    string ToTitleCase(string s)
    {
      var raw = CultureInfo.CurrentCulture.TextInfo.ToLower(s.Replace(",", ", "));
      return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(raw);
    }

    Measure CreateRawMeasure(DataRow row, double divisor, string desc)
    {
      return new Measure()
      {
        Description = desc,
        Calories = Math.Round((GetData<double>(row, "Energ_Kcal") / 100) * divisor, 1),
        Carbohydrates = Math.Round((GetData<double>(row, "Carbohydrt") / 100) * divisor, 1),
        Cholestrol = Math.Round((GetData<double>(row, "Cholestrl") / 100) * divisor, 1),
        Fiber = Math.Round((GetData<double>(row, "Fiber_TD") / 100) * divisor, 1),
        Iron = Math.Round((GetData<double>(row, "Iron") / 100) * divisor, 1),
        Protein = Math.Round((GetData<double>(row, "Protein") / 100) * divisor, 1),
        SaturatedFat = Math.Round((GetData<double>(row, "FA_Sat") / 100) * divisor, 1),
        Sodium = Math.Round((GetData<double>(row, "Sodium") / 100) * divisor, 1),
        Sugar = Math.Round((GetData<double>(row, "Sugar_Tot") / 100) * divisor, 1),
        TotalFat = Math.Round((GetData<double>(row, "Lipid_Tot") / 100) * divisor, 1)
      };
    }

    T GetData<T>(DataRow row, string name)
    {
      var result = row[name];
      if (result == DBNull.Value) return default(T);
      return (T)result;
    }

    DataTable OpenExcel(string path, string sheet)
    {
      var filename = HostingEnvironment.MapPath(path);
      var cs = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0", filename);
      OleDbConnection conn = new OleDbConnection(cs);
      conn.Open();

      OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}$]", sheet), conn);
      OleDbDataAdapter da = new OleDbDataAdapter();
      da.SelectCommand = cmd;

      System.Data.DataTable dt = new System.Data.DataTable();
      da.Fill(dt);
      conn.Close();

      return dt;

    }
  }
}
