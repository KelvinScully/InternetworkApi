﻿using ACommon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITestDalService
    {
        Task<ApiResult<TestItemDto>> SpTestItemGet(int testItemId);
    }
}
