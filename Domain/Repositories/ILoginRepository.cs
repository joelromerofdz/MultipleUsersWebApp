using Shared.Dtos.Users;

namespace Domain.Repositories
{
    public interface ILoginRepository
    {
        Task<UserResponse?> GetByEmailAsync(string email, string password);
    }
}
