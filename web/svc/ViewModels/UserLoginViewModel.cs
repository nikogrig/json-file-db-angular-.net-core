using Newtonsoft.Json;

namespace src.ViewModels
{
    public class UserLoginViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
