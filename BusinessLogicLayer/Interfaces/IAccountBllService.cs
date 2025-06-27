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
        Task<ApiResult<User>> Authenticate(Authenticate user);
        Task<ApiResult<User>> Register(Register user);
    }
}
