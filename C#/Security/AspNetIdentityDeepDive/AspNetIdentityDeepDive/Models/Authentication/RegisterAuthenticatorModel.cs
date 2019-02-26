using System.ComponentModel.DataAnnotations;

namespace AspNetIdentityDeepDive.Models.Authentication
{
    public class RegisterAuthenticatorModel
    {
        [Required] public string Code { get; set; }

        [Required] public string AuthenticatorKey { get; set; }
    }
}