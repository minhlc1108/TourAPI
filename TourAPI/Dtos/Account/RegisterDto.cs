using System;
using System.ComponentModel.DataAnnotations;

namespace TourAPI.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? CustomerName { get; set; }

        [Required]
        public int CustomerSex { get; set; }

        [Required]
        public string? CustomerAddress { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
