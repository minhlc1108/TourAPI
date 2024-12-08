using System.ComponentModel.DataAnnotations;

namespace TourAPI.Dtos.Customer
{
    public class CreateCustomerReqDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int Sex { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

    }
}
