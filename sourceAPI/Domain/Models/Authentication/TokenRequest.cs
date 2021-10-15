using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiServ.Domain.Models.Authentication
{
    public class TokenRequest
    {
        [Required]
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
