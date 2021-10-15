using ApiServ.Domain.Models.Authentication;

namespace ApiServ.Domain.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
    }
}
