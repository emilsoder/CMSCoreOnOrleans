using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CMSCore.Identity.Models.ManageViewModels
{
    public class IdentityUserViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
