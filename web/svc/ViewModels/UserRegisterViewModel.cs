using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace src.ViewModels
{
    public class UserRegisterViewModel
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
