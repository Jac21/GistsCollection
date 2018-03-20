using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Shouts()
        {
            // NEVER DO THIS
            var username = HttpContext.Request.Cookies["username"]?.ToString();
            // NEVER DO THIS
            var password = HttpContext.Request.Cookies["password"]?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("login");
            }

            using (var client = new HttpClient())
            {
                var shoutsResponse = await (await client.GetAsync($"http://localhost:33917/api/shouts?username={username}&password={password}")).Content.ReadAsStringAsync();

                var shouts = JsonConvert.DeserializeObject<Shout[]>(shoutsResponse);
                
                return View(shouts);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // NEVER DO THIS
            // NEVER DO THIS
            // NEVER DO THIS
            // NEVER DO THIS
            HttpContext.Response.Cookies.Append("username", username);

            // NEVER DO THIS
            // NEVER DO THIS
            // NEVER DO THIS
            HttpContext.Response.Cookies.Append("password", password);

            return RedirectToAction("Shouts");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
