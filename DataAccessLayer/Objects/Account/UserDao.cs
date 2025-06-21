using ACommon.Objects;
using ACommon.Objects.Account;
using DataAccessLayer.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using MicrosoftSqlParameter = Microsoft.Data.SqlClient.SqlParameter;

namespace DataAccessLayer.Objects.Account
{
    internal interface IUserDao
    {
        Task<ApiResult<UserDao>> GetSingleAsync(DataAccessService dal, string storeProcedure, List<Services.SqlParameter> parameters, Dictionary<string, string> expectedColumns);
    }

    internal class UserDao : IUserDao
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
        public byte[] UserHash { get; set; } = [];
        public byte[] UserSalt { get; set; } = [];
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }

        public async Task<ApiResult<UserDao>> GetSingleAsync(DataAccessService dal, string storeProcedure, List<Services.SqlParameter> parameters, Dictionary<string, string> expectedColumns)
        {
            UserDao userDao = new();
            bool userFound = false;

            try
            {
                using (var connection = new SqlConnection(dal.ConnectionString))
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
                            userDao.UserId = DataReaderHelper.Get<int>(reader, expectedColumns["UserId"]);
                            userDao.UserName = DataReaderHelper.Get<string>(reader, expectedColumns["UserName"]);
                            userDao.UserRoles = DataReaderHelper.Get<string>(reader, expectedColumns["UserRoles"]);
                            userDao.UserEmail = DataReaderHelper.Get<string>(reader, expectedColumns["UserEmail"]);
                            userDao.IsEmailVerified = DataReaderHelper.Get<bool>(reader, expectedColumns["IsEmailVerified"]);
                            userDao.IsActive = DataReaderHelper.Get<bool>(reader, expectedColumns["IsActive"]);


                            userFound = true;
                        }
                    }
                }

                if (userFound)
                {
                    return new ApiResult<UserDao>()
                    {
                        IsSuccessful = true,
                        Value = userDao,
                        Message = "User Found"
                    };
                }
                else
                {
                    return new ApiResult<UserDao>()
                    {
                        IsSuccessful = false,
                        Value = userDao,
                        Message = "User not found with that Id"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<UserDao>
                {
                    IsSuccessful = false,
                    Value = new UserDao(),
                    Message = $"Error in the Database: {ex}"
                };
            }
        }
    }
}
