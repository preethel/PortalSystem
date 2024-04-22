using System.ComponentModel.DataAnnotations;

namespace PortalSystem.Api.Dto
{
    public class RegistrationRequest
    {
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Name must be at least 4 characters long")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be at least 4 characters long")]
        public string Password { get; set; } = string.Empty;
    }
}
