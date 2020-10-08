using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class RegisterDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; } = default!;
        
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(80)]
        public string FirstName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(80)]
        public string LastName { get; set; } = default!;
        
        [MaxLength(20)]
        public string? NationalIdNumber { get; set; }
        
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        
        [MaxLength(100)]
        public string DisplayName { get; set; } = default!;
    }
}