using CleanArchitecture.Applicattion.Models.Identity;

namespace CleanArchitecture.Applicattion.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);

    }
}
