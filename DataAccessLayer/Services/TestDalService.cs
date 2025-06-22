using ACommon;
using ACommon.Objects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftSqlParameter = Microsoft.Data.SqlClient.SqlParameter;

namespace DataAccessLayer.Services
{
    internal class TestDalService: DataAccessService, ITestDalService
    {
        public TestDalService(ConnectionOptions connectionOptions) : base(connectionOptions)
        { }

        public async Task<ApiResult<TestItemDto>> SpTestItemGet(int testItemId)
        {
            string storeProcedure = "dbo.SpTestItemGet";
            var parameters = new List<SqlParameter>
            {
                new("@testItemId", testItemId)
            };
            Dictionary<string, string> expectedColumns = new()
            {
                ["TestItemId"] = "TestItemId",
                ["Name"] = "Name",
                ["CreatedAt"] = "CreatedAt"
            };


            TestItemDto testItemDto = new();
            bool testItem = false;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(storeProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(new MicrosoftSqlParameter(parameter.Name, parameter.Value));
                        }

                        using var reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            testItemDto.TestItemId = DataReaderHelper.Get<int>(reader, expectedColumns["TestItemId"]);
                            testItemDto.Name = DataReaderHelper.Get<string>(reader, expectedColumns["Name"]);
                            testItemDto.CreatedAt = DataReaderHelper.Get<DateTime>(reader, expectedColumns["CreatedAt"]);

                            testItem = true;
                        }
                    }
                }

                if (testItem)
                {
                    return new ApiResult<TestItemDto>()
                    {
                        IsSuccessful = true,
                        Value = testItemDto,
                        Message = "Test Item Found"
                    };
                }
                else
                {
                    return new ApiResult<TestItemDto>()
                    {
                        IsSuccessful = false,
                        Value = testItemDto,
                        Message = "Test Item not found with that Id"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<TestItemDto>
                {
                    IsSuccessful = false,
                    Value = new TestItemDto(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
    }
}
