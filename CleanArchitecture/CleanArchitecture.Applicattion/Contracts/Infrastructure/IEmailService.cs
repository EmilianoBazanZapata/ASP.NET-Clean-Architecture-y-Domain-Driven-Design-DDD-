using CleanArchitecture.Applicattion.Models;

namespace CleanArchitecture.Applicattion.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
