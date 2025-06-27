using ACommon.Objects;
using ACommon.Objects.Account;
using BusinessLogicLayer.Objects.Account;
using DataAccessLayer.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ManualMappings.Account
{
    internal static partial class ManualMapping
    {
        public static User FromDto(UserDto result)
        {
            return new User()
            {
                UserId = result.UserId,
                Username = result.Username,
                UserRoles = result.ListRoles(result.UserRoles),
                UserEmail = result.UserEmail,
                IsEmailVerified = result.IsEmailVerified,
                IsActive = result.IsActive,
            };
        }
    }
}
