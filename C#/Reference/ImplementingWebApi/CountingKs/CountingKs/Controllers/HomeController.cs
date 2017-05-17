using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CountingKs.Data;

namespace CountingKs.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var repo = new CountingKsRepository(new CountingKsContext());

      var results = repo.GetAllFoodsWithMeasures().Take(25).ToList();

      return View(results);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }
  }
}
