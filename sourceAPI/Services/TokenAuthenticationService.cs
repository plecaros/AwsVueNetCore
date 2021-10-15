using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ApiServ.Domain.Models.Authentication;
using ApiServ.Domain.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiServ.Services
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly ITokenManagementService _tokenManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(ITokenManagementService tokenManagementService, IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagementService = tokenManagementService;
            _tokenManagement = tokenManagement.Value;
        }

        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            token = string.Empty;
            if (!_tokenManagementService.IsValidUser(request)) return false;

            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.Token)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires:DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
