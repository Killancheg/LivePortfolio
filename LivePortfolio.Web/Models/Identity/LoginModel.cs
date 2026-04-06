using System.ComponentModel.DataAnnotations;

namespace LivePortfolio.Web.Models.Identity
{
    public record LoginModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
