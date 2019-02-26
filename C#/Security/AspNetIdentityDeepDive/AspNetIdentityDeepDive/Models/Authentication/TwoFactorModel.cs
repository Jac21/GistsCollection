using System.ComponentModel.DataAnnotations;

namespace AspNetIdentityDeepDive.Models.TwoFactor
{
    public class TwoFactorModel
    {
        [Required] public string Token { get; set; }
    }
}