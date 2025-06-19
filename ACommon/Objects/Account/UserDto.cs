using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACommon.Objects.Account
{
    /// <summary>
    /// DTO Class:
    /// Represents a pure data-transfer object (DTO) with no behavior or logic.
    /// Used for serializing and transporting raw data (e.g., as JSON) between layers or systems.
    /// Supports flat data structures, including primitive types, List<> and arrays.
    /// Avoid deep or nested object graphs for simplicity and compatibility across clients.
    /// </summary>
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
        public byte[] UserHash { get; set; } = [];
        public byte[] UserSalt { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
    }
}
