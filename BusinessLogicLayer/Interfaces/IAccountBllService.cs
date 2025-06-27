using ACommon.Objects;
using ACommon.Objects.Account;
using BusinessLogicLayer.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountBllService
    {
        Task<ApiResult<bool>> Authenticate(Authenticate authenticate);
        Task<ApiResult<User>> UserGetById(UserGetById users);
        Task<ApiResult<List<User>>> UsersGetByIds(List<UserGetById> users);
        Task<ApiResult<User>> UserGetByUsernameAndPassword(UserGetByUsernameAndPassword users);
        Task<ApiResult<User>> UserInsert(UserInsert user);
        Task<ApiResult<User>> UserUpdate(UserUpdate user);
    }
}
