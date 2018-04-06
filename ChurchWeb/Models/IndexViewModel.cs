using System.Collections.Generic;
using ChurchWebEntities;
using System.ComponentModel.DataAnnotations;

namespace ChurchWeb.Models
{
    public class IndexViewModel : LayoutViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        public List<CarouselItem> CarouselItems { get; set; }
    }
}
