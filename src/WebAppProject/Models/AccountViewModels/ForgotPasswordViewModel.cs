using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
