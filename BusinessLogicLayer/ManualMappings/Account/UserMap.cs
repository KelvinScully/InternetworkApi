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
        public static UserDto ToDto(UserBlo result)
        {
            return new UserDto()
            {
                UserId = result.UserId,
                UserName = result.UserName,
                UserRoles = result.UserRoles,
                UserEmail = result.UserEmail,
                IsEmailVerified = result.IsEmailVerified,
                IsActive = result.IsActive,
            };
        }
        public static ApiResult<UserDto> ToDto(ApiResult<UserBlo> result)
        {
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
        public static UserBlo ToBlo(UserDto result)
        {
            return new UserBlo()
            {
                UserId = result.UserId,
                UserName = result.UserName,
                UserRoles = result.UserRoles,
                UserEmail = result.UserEmail,
                IsEmailVerified = result.IsEmailVerified,
                IsActive = result.IsActive,
            };
        }

        public static ApiResult<UserBlo> ToBlo(ApiResult<UserDto> result)
        {
            return new ApiResult<UserBlo>
            {
                IsSuccessful = result.IsSuccessful,
                Value = new UserBlo()
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
