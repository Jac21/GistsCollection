using MyJwtCreator.Models;
using MyJwtCreator.Token;
using System;
using System.Threading.Tasks;

namespace MyJwtCreator
{
    public class Program
    {
        static void Main(string[] args)
        {
            AsyncMain(args).Wait();
        }

        public static async Task AsyncMain(string[] args)
        {
            var user = new User { UserId = 1, EmailAddress = "cat@houseofcat.io", FirstName = "House", LastName = "Cat" };
            var userData = new UserData { AccountId = 1 };
            var issuer = "https://houseofcat.io";
            var authority = "https://houseofcat.io"; // or https://authorityprovider.com/ etc

            //Issuer & Authority Note: If your app or API is the issuer and authority they will be the exact same field. They but don't have to be, key distinction.

            //256-bit string generated on https://passwordsgenerator.net/
            var privateKey = "J6k2eVCTXDp5b97u6gNH5GaaqHDxCmzz2wv3PRPFRsuW2UavK8LGPRauC4VSeaetKTMtVmVzAC8fh8Psvp8PFybEvpYnULHfRpM8TA2an7GFehrLLvawVJdSRqh2unCnWehhh2SJMMg5bktRRapA8EGSgQUV8TCafqdSEHNWnGXTjjsMEjUpaxcADDNZLSYPMyPSfp6qe5LMcd5S9bXH97KeeMGyZTS2U8gp3LGk2kH4J4F3fsytfpe9H9qKwgjb";
            var daysValid = 7;

            var createJwt = await TokenHelper.CreateJwtAsync(user, userData, issuer, authority, privateKey, daysValid);

            await Console.Out.WriteLineAsync(createJwt);
            await Console.In.ReadLineAsync();
        }
    }
}
