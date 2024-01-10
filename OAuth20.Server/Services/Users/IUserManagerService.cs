using OAuth20.Server.OauthRequest;
using OAuth20.Server.OauthResponse;

namespace OAuth20.Server.Services.Users
{
    public interface IUserManagerService
    {
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task<OpenIdConnectLoginResponse> LoginUserByOpenIdAsync(OpenIdConnectLoginRequest request);
    }
}