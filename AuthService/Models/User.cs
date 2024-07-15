namespace AuthService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; } = Guid.NewGuid().ToString();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
