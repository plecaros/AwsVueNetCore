using ApiServ.Domain.Models.Authentication;
using ApiServ.Domain.Repositories;
using ApiServ.Domain.Services;

namespace ApiServ.Services
{
    public class TokenManagementService : ITokenManagementService
    {
        private readonly ITokenManagementRepository _tokenManagementRepository;

        public TokenManagementService(ITokenManagementRepository tokenManagementRepository)
        {
            _tokenManagementRepository = tokenManagementRepository;
        }

        public bool IsValidUser(TokenRequest token)
        {
            return _tokenManagementRepository.GetClientAuthentication(token);
        }
    }
}
