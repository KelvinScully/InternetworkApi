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
        public async Task<ApiResult<bool>> Authenticate(Authenticate authenticate)
        {
            return new ApiResult<bool>
            {
                IsSuccessful = false,
                Value = false,
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
            if (string.IsNullOrEmpty(authenticate.Username) || string.IsNullOrEmpty(authenticate.UserPassword))
                return new ApiResult<bool> { IsSuccessful = false, Value = false, Message = "Username or Password is null or empty", HttpStatusCode = StatusCodes.Status400BadRequest };

            try
            {
                
            }
            catch 
            {

            }
        }
        public async Task<ApiResult<User>> UserGetById(UserGetById users)
        {
            return new ApiResult<User>
            {
                IsSuccessful = false,
                Value = new User(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<List<User>>> UsersGetByIds(List<UserGetById> users)
        {
            return new ApiResult<List<User>>
            {
                IsSuccessful = false,
                Value = new List<User>(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<User>> UserGetByUsernameAndPassword(UserGetByUsernameAndPassword users)
        {
            return new ApiResult<User>
            {
                IsSuccessful = false,
                Value = new User(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
        public async Task<ApiResult<User>> UserInsert(UserInsert user)
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

                var dalResult = await _AccountDal.UserInsert(new UserDto() { Username = user.Username, UserHash = userHash, UserSalt = userSalt, UserEmail = user.UserEmail });

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
                    HttpStatusCode= StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ApiResult<User>> UserUpdate(UserUpdate user)
        {
            return new ApiResult<User>
            {
                IsSuccessful = false,
                Value = new User(),
                Message = $"Not Set up",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
