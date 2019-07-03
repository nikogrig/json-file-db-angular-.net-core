using System.Collections.Generic;
using Newtonsoft.Json;

namespace src.DataAccess.DTO
{
    public class Root
    {
        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
