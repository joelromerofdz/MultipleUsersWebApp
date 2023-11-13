using Application.Services.Interfaces.Users;
using Domain.Entities;
using Domain.Repositories;
using Shared.Dtos.User;
using Shared.Dtos.Users;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, UserResponse> _userRepository;
        private readonly ILoginRepository _loginRepository;

        public UserService(
            IRepository<User, UserResponse> userRepository,
            ILoginRepository loginRepository)
        {
            this._userRepository = userRepository;
            this._loginRepository = loginRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserResponse>> GetUsersByCountryAsync(string country)
        {
            return await _userRepository.GetByAsync(country);
        }

        //TODO: LOGIN AND INSTALL JWT TOKEN
        public async Task<AunthenticationResult> LoginAsync(string email, string password)
        {
            var userResult = await _loginRepository.GetByEmailAsync(email, password);

            if (userResult is not UserResponse user)
            {
                throw new Exception("Invalid user.");
            }

            var token = "";
            return new AunthenticationResult(user, token);
        }

        public async Task AddMultipleUsersAsync(IEnumerable<UserRequest> userRequests)
        {
            if (userRequests == null || !userRequests.Any())
            {
                return;
            }

            IEnumerable<User> users = userRequests.Select(userRequest => new User
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                PasswordUser = userRequest.PasswordUser,
                CountryId = userRequest.CountryId,
                ProvinceId = userRequest.ProvinceId,
                CityId = userRequest.CityId
            });

            await _userRepository.AddMultipleAsync(users);
        }

    }
}