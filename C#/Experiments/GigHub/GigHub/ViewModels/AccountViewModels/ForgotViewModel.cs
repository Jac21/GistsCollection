using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels.AccountViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}