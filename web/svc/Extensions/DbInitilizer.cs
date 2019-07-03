using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using src.DataAccess;
using src.DataAccess.DTO;

namespace src.Extensions
{
    public static class DbInitializer
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider
                    .GetService<DbContext>();
          
                context.CreateFileDb();

                var adminEmail = "admin@admin.net";
                
                User adminUser = context.Items.Users.Where(a => a.Email == adminEmail).FirstOrDefault(); 
                
                if (adminUser == null)
                {
                    var rolesArr = new[] { "administrator", "customer" };
                    var users = new HashSet<User>();
                    var roles = new HashSet<Role>();
                    var userRoles = new HashSet<UserRole>();

                    var id = 1;
                    foreach (var role in rolesArr)
                    {
                        roles.Add(new Role { Id = id, AccountRole = role });
                        id++;
                    }

                    adminUser = new User
                    {
                        Id = 1,
                        UserName = "Nikola",
                        Email = adminEmail,
                        FirstName = "Nikola",
                        LastName = "Grigorov",
                        Telephone = "0882283721",
                        Password = "123456"
                    };

                    users.Add(adminUser);

                    userRoles.Add(new UserRole() { RoleId = 1, USerId = adminUser.Id });

                    context.SaveChanges(users, userRoles, roles);
                }

                return app;
            }
        }
    }
}