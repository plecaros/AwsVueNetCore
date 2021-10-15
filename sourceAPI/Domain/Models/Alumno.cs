using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServ.Domain.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string FechaCreacion { get; set; }
       
    }

    public class JsonResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string mensajeServidor { get; set; }
        public List<Alumno> data { get; set; }

    }

    public class LoginViewmodel
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

    }
}
