using ACommon.Objects.Account;
using Module.Shared.Objects.Account;

namespace Module.Shared.ManualMappings.Account
{
    internal static partial class ManualMapping
    {
        public static UserDto ToDto(User result)
        {
            return new UserDto()
            {
                UserId = result.UserId,
                UserName = result.UserName,
                UserRoles = result.ConcatRoles(),
                UserEmail = result.UserEmail,
                IsEmailVerified = result.IsEmailVerified,
                IsActive = result.IsActive,
            };
        }
        public static UserDto ToDto(UserGet result)
        {
            return new UserDto()
            {
                UserId = result.UserId,
                UserName = string.Empty,
                UserRoles = string.Empty,
                UserEmail = string.Empty,
                IsEmailVerified = default,
                IsActive = default,
            };
        }
    }
}
