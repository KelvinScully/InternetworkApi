using ACommon.Objects;
using ACommon.Objects.Account;
using DataAccessLayer.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ManualMappings.Account
{
    internal static partial class ManualMapping
    {
        public static ApiResult<UserDto> ToDto(ApiResult<UserDao> result)
        {
            // Should not return Hash or Salt
            return new ApiResult<UserDto>
            {
                IsSuccessful = result.IsSuccessful,
                Value = new UserDto()
                {
                    UserId = result.Value.UserId,
                    UserName = result.Value.UserName,
                    UserRoles = result.Value.UserRoles,
                    UserEmail = result.Value.UserEmail,
                    IsEmailVerified = result.Value.IsEmailVerified,
                    IsActive = result.Value.IsActive,
                },
                Message = result.Message
            };

        }

        public static ApiResult<UserDao> ToDao(ApiResult<UserDto> result)
        {
            return new ApiResult<UserDao>
            {
                IsSuccessful = result.IsSuccessful,
                Value = new UserDao()
                {
                    UserId = result.Value.UserId,
                    UserName = result.Value.UserName,
                    UserRoles = result.Value.UserRoles,
                    UserEmail = result.Value.UserEmail,
                    IsEmailVerified = result.Value.IsEmailVerified,
                    IsActive = result.Value.IsActive,
                },
                Message = result.Message
            };
        }
    }
}
