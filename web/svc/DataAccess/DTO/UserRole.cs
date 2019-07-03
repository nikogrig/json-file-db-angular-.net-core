using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace src.DataAccess.DTO
{
    public class UserRole
    {

        [JsonProperty("userId")]
        public int USerId { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }
}
