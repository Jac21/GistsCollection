using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using AspNetIdentityDeepDive.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityDeepDive.Stores
{
    public class MyIdentityUserStore : IUserPasswordStore<MyIdentityUser>
    {
        public static DbConnection GetOpenConnection()
        {
            var connection =
                new SqlConnection(
                    "Data Source=JCANTU\\SQL2016CIAI;Initial Catalog=MyIdentityDatabase;Integrated Security=True");

            connection.Open();

            return connection;
        }

        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(MyIdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(MyIdentityUser user, string normalizedName,
            CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "INSERT INTO MyIdentityUsers([Id]," +
                    "[UserName]," +
                    "[NormalizedUserName]," +
                    "[PasswordHash]) " +
                    "VALUES(@id, @userName, @normalizedUserName, @passwordHash)",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "UPDATE MyIdentityUsers " +
                    "SET [Id] = @id," +
                    "[UserName] = @userName," +
                    "[NormalizedUserName] = @normalizedUserName," +
                    "[PasswordHash] = @passwordHash) " +
                    "WHERE [Id] = @id",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<MyIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MyIdentityUser>(
                    "SELECT * FROM MyIdentityUsers WHERE Id = @id",
                    new {id = userId});
            }
        }

        public async Task<MyIdentityUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MyIdentityUser>(
                    "SELECT * FROM MyIdentityUsers WHERE NormalizedUserName = @normalizedUserName",
                    new {normalizedUserName});
            }
        }

        public Task SetPasswordHashAsync(MyIdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}