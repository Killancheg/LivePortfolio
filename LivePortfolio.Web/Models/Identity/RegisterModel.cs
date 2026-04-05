using System.ComponentModel.DataAnnotations;

namespace LivePortfolio.Web.Models.Identity
{
    public sealed record RegisterModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
