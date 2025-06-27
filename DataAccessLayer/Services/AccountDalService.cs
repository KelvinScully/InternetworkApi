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

        public async Task<ApiResult<bool>> SpAuthenticate(UserDto user)
        {
            return new ApiResult<bool>
            {
                IsSuccessful = false,
                Value = false,
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
            string storeProcedure = "account.SpAuthenticate";
            var parameters = new List<SqlParameter>
            {
                new("@username", user.Username),
                new("@password", user.UserHash)
            };

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

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return new ApiResult<bool> { IsSuccessful = true, Value = true, Message = "User is Authenticated", HttpStatusCode = StatusCodes.Status200Ok };
            }
            catch
            {
                return new ApiResult<bool> { IsSuccessful = false, Value = false, Message = "Username or password is incorrect", HttpStatusCode = StatusCodes.Status400BadRequest };
            }
        }
        public async Task<ApiResult<UserDto>> SpUserGetSalt(UserDto user)
        {
            return new ApiResult<UserDto>
            {
                IsSuccessful = false,
                Value = new UserDto(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<UserDto>> UserGetById(UserDto users)
        {
            return new ApiResult<UserDto>
            {
                IsSuccessful = false,
                Value = new UserDto(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<List<UserDto>>> UsersGetByIds(List<UserDto> users)
        {
            return new ApiResult<List<UserDto>>
            {
                IsSuccessful = false,
                Value = new List<UserDto>(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<UserDto>> UserGetByUsernameAndPassword(UserDto users)
        {
            return new ApiResult<UserDto>
            {
                IsSuccessful = false,
                Value = new UserDto(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<UserDto>> UserInsert(UserDto user)
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
        public async Task<ApiResult<UserDto>> UserUpdate(UserDto user)
        {
            return new ApiResult<UserDto>
            {
                IsSuccessful = false,
                Value = new UserDto(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }


        //public async Task<ApiResult<UserDto>> SpUserGet(int userId)
        //{
        //    string storeProcedure = "account.SpUserGetInfo";
        //    var parameters = new List<SqlParameter>
        //    {
        //        new("@userId", userId)
        //    };
        //    Dictionary<string, string> expectedColumns = new ()
        //    {
        //        ["UserId"] = "UserId",
        //        ["UserName"] = "UserName",
        //        ["UserRoles"] = "UserRoles",
        //        ["UserEmail"] = "UserEmail",
        //        ["IsEmailVerified"] = "IsEmailVerified",
        //        ["IsActive"] = "IsActive",
        //    };
        //
        //    try
        //    {
        //        var result = await _UserDao.GetSingleAsync(this, storeProcedure, parameters, expectedColumns);
        //        if (!result.IsSuccessful || result.Value is null)
        //        {
        //            return new ApiResult<UserDto>
        //            {
        //                IsSuccessful = false,
        //                Value = new UserDto(),
        //                Message = result.Message
        //            };
        //        }
        //        return ManualMapping.ToDto(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResult<UserDto>
        //        {
        //            IsSuccessful = false,
        //            Value = new UserDto(),
        //            Message = $"Unhandled error in '{storeProcedure}': {ex.Message}"
        //        };
        //    }
        //}
    }
}
