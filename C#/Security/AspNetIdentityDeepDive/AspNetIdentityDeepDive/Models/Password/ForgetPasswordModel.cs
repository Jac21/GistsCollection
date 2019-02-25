using System.ComponentModel.DataAnnotations;

namespace AspNetIdentityDeepDive.Models.Password
{
    public class ForgetPasswordModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}