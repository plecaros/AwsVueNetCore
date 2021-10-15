using ApiServ.Domain.Models.Authentication;

namespace ApiServ.Domain.Repositories
{
    public interface ITokenManagementRepository
    {
        bool GetClientAuthentication(TokenRequest authenticationToken);
    }
}
