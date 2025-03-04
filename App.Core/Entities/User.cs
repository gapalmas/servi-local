namespace App.Core.Entities
{
    public class User : Document
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
        public DateTime LastLogin { get; set; } = DateTime.MinValue;
        public bool IsLocked { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public User()
        {
            Roles = [];
        }
    }
}