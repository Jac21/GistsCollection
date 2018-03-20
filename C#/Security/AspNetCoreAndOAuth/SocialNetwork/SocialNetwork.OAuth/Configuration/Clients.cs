using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace SocialNetwork.OAuth.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> All()
        {
            return new[] {
                new Client
                {
                    ClientId = "fekberg",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "socialnetwork" }
                }
            };
        }
    }
    public class Users
    {
        public static List<TestUser> All()
        {
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@filipekberg.se",
                    Password = "password"
                }
            };
        }
    }

    public class ApiResources
    {
        public static IEnumerable<ApiResource> All()
        {
            return new[] {
                new ApiResource("socialnetwork", "Social Network")
            };
        }
    }
}
