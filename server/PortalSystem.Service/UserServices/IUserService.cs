using PortalSystem.Models;

namespace PortalSystem.Service.UserServices
{
    public interface IUserService
    {
        Task RemoveRefreshToken(string refreshToken);
        Task<User> CreateUser(CreateUserRequest request);
        Task<LoginUserResponse> LoginUser(LoginUserRequest request);
        Task<LoginUserResponse> RefreshToken(string refreshToken);
    }
}
