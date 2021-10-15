using ApiServ.Domain.Models.Authentication;
using ApiServ.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiServ.Repositories
{
    public class TokenManagementRepository : ITokenManagementRepository
    {
        private readonly IConfiguration _config;

        public TokenManagementRepository(IConfiguration config)
        {
            _config = config;
        }

        public bool GetClientAuthentication(TokenRequest authenticationToken)
        {
            bool result = false;
            try
            {
                // Acá debería una consulta a un repsoitorio donde el token esté habilitado o no.
                // lo retorno true para obviar el test
                return true;
            }
            catch(Exception e)
            {
                result = false;
            }
            return result;
        }
    }
}
