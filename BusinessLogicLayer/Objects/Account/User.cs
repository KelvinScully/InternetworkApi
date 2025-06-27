using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Objects.Account
{
    // Requested Objects
    public class Authenticate
    {
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
    public class UserGetById
    {
        public int UserId { get; set; }
    }
    public class UserGetByUsernameAndPassword
    {
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
    public class UserInsert
    {
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }



    public class UserUpdate
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string ConcatRoles()
        {
            return string.Join(", ", UserRoles);
        }
    }

    // Returned Objects
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
    }
}
