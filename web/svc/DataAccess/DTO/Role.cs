using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace src.DataAccess.DTO
{
    public class Role
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("role")]
        public string AccountRole { get; set; }
    }
}
