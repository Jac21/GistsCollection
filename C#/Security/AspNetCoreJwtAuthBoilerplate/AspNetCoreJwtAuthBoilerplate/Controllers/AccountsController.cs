using System.Threading.Tasks;
using AspNetCoreJwtAuthBoilerplate.Data;
using AspNetCoreJwtAuthBoilerplate.Data.Models.Entities;
using AspNetCoreJwtAuthBoilerplate.Data.ViewModels;
using AspNetCoreJwtAuthBoilerplate.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJwtAuthBoilerplate.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext appDbContext;

        private readonly UserManager<AppUser> userManager;

        private readonly IMapper mapper;

        public AccountsController(ApplicationDbContext appDbContext, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await appDbContext.Users.FindAsync();

            return new ObjectResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = mapper.Map<AppUser>(model);

            var result = await userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }

            await appDbContext.Customers.AddAsync(new Customer
            {
                IdentityId = userIdentity.Id,
                Location = model.Location
            });

            await appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created!");
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