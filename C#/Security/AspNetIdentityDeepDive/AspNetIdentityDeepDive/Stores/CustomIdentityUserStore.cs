using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityDeepDive.Stores
{
    public class CustomIdentityUserStore : UserStoreBase<IdentityUser, string, IdentityUserClaim<string>,
        IdentityUserLogin<string>, IdentityUserToken<string>>
    {
        public CustomIdentityUserStore(IdentityErrorDescriber describer) : base(describer)
        {
        }

        public override Task<IdentityResult> CreateAsync(IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> UpdateAsync(IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> DeleteAsync(IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityUser> FindByIdAsync(string userId,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUser> FindUserAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<string>> FindUserLoginAsync(string userId, string loginProvider,
            string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<string>> FindUserLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Claim>> GetClaimsAsync(IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserToken<string>> FindTokenAsync(IdentityUser user, string loginProvider,
            string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task AddUserTokenAsync(IdentityUserToken<string> token)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken<string> token)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<IdentityUser> Users { get; }

        public override Task AddLoginAsync(IdentityUser user, UserLoginInfo login,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(IdentityUser user, string loginProvider, string providerKey,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityUser> FindByEmailAsync(string normalizedEmail,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}