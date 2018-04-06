using System.ComponentModel.DataAnnotations;

namespace ChurchWeb.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
