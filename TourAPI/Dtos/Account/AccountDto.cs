namespace TourAPI.Dtos.Account
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public int? Role { get; set; }
    }
}
