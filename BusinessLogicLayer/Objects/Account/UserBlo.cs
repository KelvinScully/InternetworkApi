using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Objects.Account
{

    internal class UserBlo
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
        public byte[] UserHash { get; set; } = [];
        public byte[] UserSalt { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }

        public bool IsGetValid()
        {
            if (UserId != 0 && UserId > 0)
                return true;

            return false;
        }
    }
}
