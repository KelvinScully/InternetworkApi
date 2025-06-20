using System.Data;

namespace Module.Shared.Objects.Account
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = [];
        public string Password { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string ConcatRoles()
        {
            return string.Join(", ", UserRoles);
        }
    }

    public class UserGet
    {
        public int UserId { get; set; }
    }

    public class UserUpsert
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = [];
        public string Password { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string ConcatRoles()
        {
            return string.Join(", ", UserRoles);
        }
    }
}
