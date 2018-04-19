using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreJwtAuthBoilerplate.Data;
using AspNetCoreJwtAuthBoilerplate.Data.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreJwtAuthBoilerplate.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal caller;

        private readonly ApplicationDbContext appDbContext;

        public DashboardController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            caller = httpContextAccessor.HttpContext.User;
            this.appDbContext = appDbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            // HttpContext.User
            var userId = caller.Claims.Single(c => c.Type == "id");
            var customer = await appDbContext.Customers.Include(c => c.Identity)
                .SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                customer.Identity.FirstName,
                customer.Identity.LastName,
                customer.Identity.PictureUrl,
                customer.Identity.FacebookId,
                customer.Location,
                customer.Locale,
                customer.Gender
            });
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}