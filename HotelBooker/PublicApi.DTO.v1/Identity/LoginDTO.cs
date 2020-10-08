using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class LoginDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; } = default!;
        
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; } = default!;
    }
}