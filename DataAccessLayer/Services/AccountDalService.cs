using DataAccessLayer.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACommon.Objects;
using ACommon.Objects.Account;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ManualMappings.Account;
using ACommon;

namespace DataAccessLayer.Services
{
    internal class AccountDalService : DataAccessService, IAccountDalService
    {
        IUserDao _UserDao;
        public AccountDalService(IUserDao userDao , ConnectionOptions connectionOptions) : base(connectionOptions)
        {
            _UserDao = userDao;
        }

        public async Task<ApiResult<UserDto>> SpUserGet(int userId)
        {
            string storeProcedure = "account.SpUserGetInfo";
            var parameters = new List<SqlParameter>
            {
                new("@userId", userId)
            };
            Dictionary<string, string> expectedColumns = new ()
            {
                ["UserId"] = "UserId",
                ["UserName"] = "UserName",
                ["UserRoles"] = "UserRoles",
                ["UserEmail"] = "UserEmail",
                ["IsEmailVerified"] = "IsEmailVerified",
                ["IsActive"] = "IsActive",
            };

            try
            {
                var result = await _UserDao.GetSingleAsync(this, storeProcedure, parameters, expectedColumns);
                if (!result.IsSuccessful || result.Value is null)
                {
                    return new ApiResult<UserDto>
                    {
                        IsSuccessful = false,
                        Value = new UserDto(),
                        Message = result.Message
                    };
                }
                return ManualMapping.ToDto(result);
            }
            catch (Exception ex)
            {
                return new ApiResult<UserDto>
                {
                    IsSuccessful = false,
                    Value = new UserDto(),
                    Message = $"Unhandled error in '{storeProcedure}': {ex.Message}"
                };
            }
        }
    }
}
