using ACommon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITestBllService
    {
        Task<ApiResult<TestItemDto>> TestItemGet(int testItemId);
    }
}
