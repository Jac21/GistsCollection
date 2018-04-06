using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using SocialNetwork.Data.Repositories;
using SocialNetwork.OAuth.Helpers;

namespace SocialNetwork.OAuth.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository repository;

        public ResourceOwnerPasswordValidator(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await repository.GetAsync(context.UserName,
                HashHelper.Sha512(context.Password + context.UserName));

            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), "custom");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials");
            }
        }
    }
}