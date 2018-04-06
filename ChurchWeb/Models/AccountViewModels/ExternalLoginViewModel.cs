using System.ComponentModel.DataAnnotations;

namespace ChurchWeb.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
