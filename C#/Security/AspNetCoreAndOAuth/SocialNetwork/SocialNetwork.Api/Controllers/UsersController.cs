using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Api.Helpers;
using SocialNetwork.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : SocialNetworkApiController
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string username, string password)
        {
            var user = await userRepository.GetAsync(username, 
                HashHelper.Sha512(password + username));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("friends")]
        public async Task<IActionResult> FriendsAsync(string username, string password)
        {
            var user = await userRepository.GetAsync(username, HashHelper.Sha512(password + username));

            if (user == null)
            {
                return NotFound();
            }

            var friends = (await userRepository.GetFriendsForAsync(user)).ToArray();

            if (!friends.Any())
            {
                return NotFound();
            }
            
            return Ok(friends);
        }

    }
}