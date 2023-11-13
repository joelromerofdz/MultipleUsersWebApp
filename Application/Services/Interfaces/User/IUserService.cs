using Shared.Dtos.User;
using Shared.Dtos.Users;

namespace Application.Services.Interfaces.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetUsersAsync();
        Task<IEnumerable<UserResponse>> GetUsersByCountryAsync(string country);
        Task AddMultipleUsersAsync(IEnumerable<UserRequest> userRequests);
        Task<AunthenticationResult> LoginAsync(string email, string password);
    }
}
