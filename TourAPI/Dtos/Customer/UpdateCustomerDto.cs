using System.ComponentModel.DataAnnotations;

public class UpdateCustomerDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    public int Sex { get; set; }

    [Required]
    [StringLength(255)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public DateTime Birthday { get; set; }

}
