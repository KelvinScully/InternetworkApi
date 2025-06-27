using ACommon;
using ACommon.Objects;
using ACommon.Objects.Account;
using DataAccessLayer.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using MicrosoftSqlParameter = Microsoft.Data.SqlClient.SqlParameter;

namespace DataAccessLayer.Services
{
    internal class AccountDalService : DataAccessService, IAccountDalService
    {
        public AccountDalService(ConnectionOptions connectionOptions) : base(connectionOptions)
        { }

        public async Task<ApiResult<UserDto>> SpUserInsert(UserDto user)
        {
            string storeProcedure = "account.SpUserInsert";
            var parameters = new List<SqlParameter>
            {
                new("@username", user.Username),
                new("@userRoles", "User"),
                new("@userHash", user.UserHash),
                new("@userSalt", user.UserSalt),
                new("@userEmail", user.UserEmail)
            };
            Dictionary<string, string> expectedColumns = new()
            {
                ["UserId"] = "UserId",
                ["Username"] = "Username",
                ["UserRoles"] = "UserRoles",
                ["UserEmail"] = "UserEmail",
                ["IsEmailVerified"] = "IsEmailVerified",
                ["IsActive"] = "IsActive"
            };

            UserDto userDto = new();
            bool hasUser = false;
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
                            userDto.UserId = DataReaderHelper.Get<int>(reader, expectedColumns["UserId"]);
                            userDto.Username = DataReaderHelper.Get<string>(reader, expectedColumns["Username"]);
                            userDto.UserRoles = DataReaderHelper.Get<string>(reader, expectedColumns["UserRoles"]);
                            userDto.UserEmail = DataReaderHelper.Get<string>(reader, expectedColumns["UserEmail"]);
                            userDto.IsEmailVerified = DataReaderHelper.Get<bool>(reader, expectedColumns["IsEmailVerified"]);
                            userDto.IsActive = DataReaderHelper.Get<bool>(reader, expectedColumns["IsEmailVerified"]);

                            hasUser = true;
                        }
                    }
                }

                if (hasUser)
                {
                    return new ApiResult<UserDto>()
                    {
                        IsSuccessful = true,
                        Value = userDto,
                        Message = "User Created",
                        HttpStatusCode = StatusCodes.Status200Ok
                    };
                }
                else
                {
                    return new ApiResult<UserDto>()
                    {
                        IsSuccessful = false,
                        Value = userDto,
                        Message = "User failed to create",
                        HttpStatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<UserDto>
                {
                    IsSuccessful = false,
                    Value = new UserDto(),
                    Message = $"Error in the Database: {ex}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ApiResult<UserDto>> SpUserGetByUsername(UserDto user)
        {
            string storeProcedure = "account.SpUserGetByUsername";
            var parameters = new List<SqlParameter>
            {
                new("@username", user.Username)
            };
            Dictionary<string, string> expectedColumns = new()
            {
                ["UserId"] = "UserId",
                ["Username"] = "Username",
                ["UserRoles"] = "UserRoles",
                ["UserEmail"] = "UserEmail",
                ["IsEmailVerified"] = "IsEmailVerified",
                ["IsActive"] = "IsActive"
            };

            UserDto userDto = new();
            bool hasUser = false;
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
                            userDto.UserId = DataReaderHelper.Get<int>(reader, expectedColumns["UserId"]);
                            userDto.Username = DataReaderHelper.Get<string>(reader, expectedColumns["Username"]);
                            userDto.UserRoles = DataReaderHelper.Get<string>(reader, expectedColumns["UserRoles"]);
                            userDto.UserEmail = DataReaderHelper.Get<string>(reader, expectedColumns["UserEmail"]);
                            userDto.IsEmailVerified = DataReaderHelper.Get<bool>(reader, expectedColumns["IsEmailVerified"]);
                            userDto.IsActive = DataReaderHelper.Get<bool>(reader, expectedColumns["IsActive"]);

                            hasUser = true;
                        }
                    }
                }

                if (hasUser)
                {
                    return new ApiResult<UserDto>()
                    {
                        IsSuccessful = true,
                        Value = userDto,
                        Message = "User found",
                        HttpStatusCode = StatusCodes.Status200Ok
                    };
                }
                else
                {
                    return new ApiResult<UserDto>()
                    {
                        IsSuccessful = false,
                        Value = userDto,
                        Message = "User not found",
                        HttpStatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<UserDto>
                {
                    IsSuccessful = false,
                    Value = new UserDto(),
                    Message = $"Error in the Database: {ex}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ApiResult<UserDto>> SpUserGetHashNSalt(UserDto user)
        {
            string storeProcedure = "account.SpUserGetHashNSalt";
            var parameters = new List<SqlParameter>
            {
                new("@username", user.Username)
            };
            Dictionary<string, string> expectedColumns = new()
            {
                ["Username"] = "Username",
                ["UserHash"] = "UserHash",
                ["UserSalt"] = "UserSalt"
            };

            UserDto userDto = new();
            bool hasUser = false;
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
                            userDto.Username = DataReaderHelper.Get<string>(reader, expectedColumns["Username"]);
                            userDto.UserHash = DataReaderHelper.Get<byte[]>(reader, expectedColumns["UserHash"]);
                            userDto.UserSalt = DataReaderHelper.Get<byte[]>(reader, expectedColumns["UserSalt"]);

                            hasUser = true;
                        }
                    }
                }

                if (hasUser)
                {
                    return new ApiResult<UserDto>()
                    {
                        IsSuccessful = true,
                        Value = userDto,
                        Message = "User found",
                        HttpStatusCode = StatusCodes.Status200Ok
                    };
                }
                else
                {
                    return new ApiResult<UserDto>()
                    {
                        IsSuccessful = false,
                        Value = userDto,
                        Message = "User not found",
                        HttpStatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<UserDto>
                {
                    IsSuccessful = false,
                    Value = new UserDto(),
                    Message = $"Error in the Database: {ex}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
