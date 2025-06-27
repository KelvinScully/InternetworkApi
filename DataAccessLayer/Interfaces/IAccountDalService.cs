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
        Task<ApiResult<UserDto>> SpUserInsert(UserDto user);
        Task<ApiResult<UserDto>> SpUserGetByUsername(UserDto users);
        Task<ApiResult<UserDto>> SpUserGetHashNSalt(UserDto user);
    }
}
