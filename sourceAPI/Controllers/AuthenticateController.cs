using ApiServ.Domain.Models;
using ApiServ.Domain.Models.Authentication;
using ApiServ.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ApiServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authService;
        public AuthenticateController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Obtiene token JWT que permite autorizar el consumo de los endpoints que necesiten Authorization de este API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("RequestToken")]
        [HttpPost]
        public ActionResult RequestToken(string request)
        {

            TokenRequest tokenReq = new TokenRequest();

            tokenReq.Token = request;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Valido el token enviado por parámetro para retornar un token JWT en el caso de ser válido
            string token;
            if (_authService.IsAuthenticated(tokenReq, out token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");

        }

        [Route("Login")]
        [HttpPost]
        public List<JsonResponse> Login(string userName, string login_password, string request)
        {
            List<JsonResponse> validaciones = new List<JsonResponse>();
            TokenRequest tokenReq = new TokenRequest();

            tokenReq.Token = request;
            string token;
            //Para efectos de test dejo este usuario
            if (userName == "Pedro" && login_password == "123456789")
            {
                if (_authService.IsAuthenticated(tokenReq, out token))
                {
                    validaciones.Add(new JsonResponse { success = true, message = token, mensajeServidor = HttpStatusCode.OK.ToString() });
                    return validaciones;
                }
                else
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "No hay registros asociados.", mensajeServidor = HttpStatusCode.NotFound.ToString() });
                        return validaciones;
                    }  
            }
            else {
                validaciones.Add(new JsonResponse { success = false, message = "No hay registros asociados.", mensajeServidor = HttpStatusCode.NotFound.ToString() });
                return validaciones;
            }

        }

    }
}