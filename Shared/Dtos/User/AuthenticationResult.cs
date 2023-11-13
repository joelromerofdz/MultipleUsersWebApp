using Shared.Dtos.Users;

namespace Shared.Dtos.User
{
    public record AunthenticationResult
    (
        UserResponse User,
        string Token
    );
}
