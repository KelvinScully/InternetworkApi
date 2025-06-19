using ACommon.Objects;
using ACommon.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountBllService
    {
        Task<ApiResult<UserDto>> UserGet(UserDto user);
    }
}
