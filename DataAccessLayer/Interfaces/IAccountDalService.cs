using ACommon.Objects;
using ACommon.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountDalService
    {
        Task<ApiResult<bool>> SpAuthenticate(UserDto user);
        Task<ApiResult<UserDto>> SpUserGetSalt(UserDto user);
        Task<ApiResult<UserDto>> UserGetById(UserDto users);
        Task<ApiResult<List<UserDto>>> UsersGetByIds(List<UserDto> users);
        Task<ApiResult<UserDto>> UserGetByUsernameAndPassword(UserDto users);
        Task<ApiResult<UserDto>> UserInsert(UserDto user);
        Task<ApiResult<UserDto>> UserUpdate(UserDto user);
    }
}
