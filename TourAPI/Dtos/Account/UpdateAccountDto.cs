namespace TourAPI.Dtos.Account
{
    public class UpdateAccountDto
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
