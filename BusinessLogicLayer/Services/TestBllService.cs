using ACommon.Objects;
using ACommon.Objects.Account;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.ManualMappings.Account;
using BusinessLogicLayer.Objects.Account;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    internal class TestBllService : ITestBllService
    {
        private ITestDalService _TestDal;
        public TestBllService(ITestDalService testDalService)
        {
            _TestDal = testDalService;
        }

        public async Task<ApiResult<TestItemDto>> TestItemGet(int testItemId)
        {
            if (testItemId <= 0)
                return new ApiResult<TestItemDto>
                {
                    IsSuccessful = false,
                    Value = new TestItemDto(),
                    Message = "Id is less then or equal to 0"
                };

            try
            {
                var dalResult = await _TestDal.SpTestItemGet(testItemId);

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<TestItemDto>
                    {
                        IsSuccessful = false,
                        Value = new TestItemDto(),
                        Message = $"DAL Failed: {dalResult.Message}"
                    };
                }

                return new ApiResult<TestItemDto>
                {
                    IsSuccessful = true,
                    Value = dalResult.Value,
                    Message = "User Found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<TestItemDto>
                {
                    IsSuccessful = false,
                    Value = new TestItemDto(),
                    Message = $"Unhandled Exception: {ex.Message}"
                };
            }
        }
    }
}
