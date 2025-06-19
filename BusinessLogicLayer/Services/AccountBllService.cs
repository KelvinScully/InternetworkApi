using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using BusinessLogicLayer.Interfaces;
using ACommon.Objects;
using ACommon.Objects.Account;
using BusinessLogicLayer.Objects.Account;
using BusinessLogicLayer.ManualMappings.Account;

namespace BusinessLogicLayer.Services
{
    internal class AccountBllService : IAccountBllService
    {
        private IAccountDalService _AccountDal;
        public AccountBllService(IAccountDalService accountDalService) 
        {
            _AccountDal = accountDalService;
        }

        public async Task<ApiResult<UserDto>> UserGet(UserDto user)
        {
            UserBlo userBlo = ManualMapping.ToBlo(user);
            if (!userBlo.IsGetValid())
                return new ApiResult<UserDto>
                {
                    IsSuccessful = false,
                    Message = "Id is less then or equal to 0"
                };

            try
            {
                var dalResult = ManualMapping.ToBlo(await _AccountDal.SpUserGet(userBlo.UserId));

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<UserDto>
                    {
                        IsSuccessful = false,
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<UserDto>
                {
                    IsSuccessful = true,
                    Value = ManualMapping.ToDto(dalResult.Value),
                    Message = "User Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<UserDto>
                {
                    IsSuccessful = false,
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
    }
}
