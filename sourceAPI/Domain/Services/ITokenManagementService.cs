using ApiServ.Domain.Models.Authentication;

namespace ApiServ.Domain.Services
{
    public interface ITokenManagementService
    {
        bool IsValidUser(TokenRequest token);
    }
}
