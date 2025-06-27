using ACommon.Objects;
using ACommon.Objects.Account;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.ManualMappings.Account;
using BusinessLogicLayer.Objects.Account;
using DataAccessLayer.Interfaces;
using System.Security.Cryptography;

namespace BusinessLogicLayer.Services
{
    internal class AccountBllService : IAccountBllService
    {
        private IAccountDalService _AccountDal;
        public AccountBllService(IAccountDalService accountDalService) 
        {
            _AccountDal = accountDalService;
        }
        public async Task<ApiResult<User>> Register(Register user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.UserPassword) || string.IsNullOrEmpty(user.UserEmail))
            {
                return new ApiResult<User>
                {
                    IsSuccessful = false,
                    Value = new()
                    {
                        Username = user.Username,
                        UserEmail = user.UserEmail,
                    },
                    Message = "Username, password, or email is null or empty",
                    HttpStatusCode = StatusCodes.Status400BadRequest
                };
            }

            try
            {
                // 128 bit salt
                byte[] userSalt = RandomNumberGenerator.GetBytes(16);
                // 256 bit hash
                using var rfc2898 = new Rfc2898DeriveBytes(user.UserPassword, userSalt, 100_000, HashAlgorithmName.SHA256);
                byte[] userHash = rfc2898.GetBytes(32);

                var dalResult = await _AccountDal.SpUserInsert(new UserDto() { Username = user.Username, UserHash = userHash, UserSalt = userSalt, UserEmail = user.UserEmail });

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<User>
                    {
                        IsSuccessful = false,
                        Value = new User(),
                        Message = $"DAL Failed: {dalResult.Message}",
                        HttpStatusCode = dalResult.HttpStatusCode
                    };
                }

                return new ApiResult<User>
                {
                    IsSuccessful = true,
                    Value = ManualMapping.FromDto(dalResult.Value),
                    Message = "User Created",
                    HttpStatusCode = dalResult.HttpStatusCode
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<User>
                {
                    IsSuccessful = false,
                    Value = new User(),
                    Message = $"Unhandled Exception: {ex.Message}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ApiResult<User>> Authenticate(Authenticate user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.UserPassword))
            {
                return new ApiResult<User>
                {
                    IsSuccessful = false,
                    Value = new()
                    {
                        Username = user.Username,
                    },
                    Message = "Username, password, or email is null or empty",
                    HttpStatusCode = StatusCodes.Status400BadRequest
                };
            }

            try
            {
                // Get the Hash n Salt
                var dalHashNSalt = await _AccountDal.SpUserGetHashNSalt(new UserDto() { Username = user.Username });
                if (!dalHashNSalt.IsSuccessful || dalHashNSalt.Value is null)
                {
                    return new ApiResult<User>
                    {
                        IsSuccessful = false,
                        Value = new User(),
                        Message = $"DAL Failed: {dalHashNSalt.Message}",
                        HttpStatusCode = dalHashNSalt.HttpStatusCode
                    };
                }
                var dalHash = dalHashNSalt.Value.UserHash;
                var dalSalt = dalHashNSalt.Value.UserSalt;

                // 256 bit hash
                using var rfc2898 = new Rfc2898DeriveBytes(user.UserPassword, dalSalt, 100_000, HashAlgorithmName.SHA256);
                byte[] userHash = rfc2898.GetBytes(32);

                if (!userHash.SequenceEqual(dalHash))
                {
                    return new ApiResult<User>
                    {
                        IsSuccessful = false,
                        Value = new User(),
                        // Only the password is wrong but we send this
                        Message = $"Username or Password is wrong",
                        HttpStatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Now we get the User Objects
                var dalResult = await _AccountDal.SpUserGetByUsername(new UserDto() { Username = user.Username });

                if (!dalResult.IsSuccessful || dalResult.Value is null)
                {
                    return new ApiResult<User>
                    {
                        IsSuccessful = false,
                        Value = new User(),
                        Message = $"DAL Failed: {dalResult.Message}",
                        HttpStatusCode = dalResult.HttpStatusCode
                    };
                }

                return new ApiResult<User>
                {
                    IsSuccessful = true,
                    Value = ManualMapping.FromDto(dalResult.Value),
                    Message = "Authenticated",
                    HttpStatusCode = dalResult.HttpStatusCode
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<User>
                {
                    IsSuccessful = false,
                    Value = new User(),
                    Message = $"Unhandled Exception: {ex.Message}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
