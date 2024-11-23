using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Customer
{
    public class UpdateCustomerReqDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int Sex { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

         [Required]
        [StringLength(255)]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}