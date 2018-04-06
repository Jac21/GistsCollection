using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialNetwork.Data.Models;
using SocialNetwork.Data.Repositories;
using SocialNetwork.OAuth.Helpers;

namespace SocialNetwork.OAuth.Configuration
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository repository;

        public UserValidator(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await repository.GetAsync(username, HashHelper.Sha512(password + username));

            return user != null;
        }

        public Task<User> FindByUsernameAsync(string username)
        {
            return repository.GetAsync(username);
        }

        public Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IUserValidator
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByExternalProviderAsync(string provider, string userId);
        Task<User> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims);
    }
}