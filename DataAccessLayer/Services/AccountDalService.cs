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

namespace DataAccessLayer.Services
{
    internal class AccountDalService : DataAccessService, IAccountDalService
    {
        public AccountDalService(string connectionString) : base(connectionString)
        { }

        public async Task<ApiResult<UserDto>> SpUserGet(int userId)
        {
            UserDao user = new();
            string storeProcedure = "account.SpUserGetInfo";
            var parameters = new List<SqlParameter>
            {
                new("@userId", userId)
            };
            var expectedColumns = new string[]
            {
                "UserId",
                "UserName",
                "UserRoles",
                "UserEmail",
                "IsEmailVerified",
                "IsActive",
            };

            try
            {
                var result = await user.GetAsync(this, storeProcedure, parameters, expectedColumns);
                if (!result.IsSuccessful || result.Value is null)
                {
                    return new ApiResult<UserDto>
                    {
                        IsSuccessful = false,
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
                    Message = $"Unhandled error in SpUserGet: {ex.Message}"
                };
            }
        }
    }
}
