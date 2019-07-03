using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using src.DataAccess.DTO;
using src.ServiceModel;

namespace src.DataAccess
{
    public class DbContext
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private const string jsonRoot = @"\DataAccess\db.json";
        
        public DbContext(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        private string Path => this._hostingEnvironment.ContentRootPath + jsonRoot;

        public Root Items { get; set; } = new Root();

        public HashSet<User> Users { get; set; } = new HashSet<User>();

        public HashSet<Role> Roles { get; set; } = new HashSet<Role>();

        public HashSet<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public int GenerateId { get => this.Items.Users.OrderByDescending(a => a.Id).Select(a => a.Id).FirstOrDefault() + 1; }

        public void CreateFileDb()
        {
            if (!this.CheckPathExist())
                File.Create(this.Path).Dispose();
        }

        public UserServiceModel Create(User userDto)
        {
            var newUserRole = new UserRole()
            {
                RoleId = this.Roles
                                 .Where(a => a.AccountRole == "customer")
                                 .Select(a => a.Id)
                                 .FirstOrDefault(),
                USerId = userDto.Id
            };

            this.Users.Add(userDto);

            this.UserRoles.Add(newUserRole);

            string role = this.Roles.Where(r => r.Id == this.UserRoles
                                                            .Where(ur => ur.USerId == userDto.Id)
                                                            .Select(ur => ur.RoleId).FirstOrDefault()
                                                ).Select(r => r.AccountRole)
                                                 .FirstOrDefault();

            this.SaveChanges(this.Users, this.UserRoles);

            var model = new UserServiceModel
            {
                User = userDto,
                Role = role
            };

            return model;
        }

        public void SaveChanges(HashSet<User> users, HashSet<UserRole> userRoles)
        {
            this.SaveChanges(users, userRoles, null);
        }

        public void SaveChanges(HashSet<User> users, HashSet<UserRole> userRoles, HashSet<Role> roles)
        {
            this.Items.Users = users.OrderBy(a => a.Id)
                                   .ToHashSet();

            this.Items.UserRoles = userRoles.OrderBy(a => a.RoleId)
                                           .ThenBy(a => a.USerId)
                                           .ToHashSet(); ;

            if (roles != null)
            {
                this.Items.Roles = roles;
            }

            this.WriteJson<Root>(this.Items);

            this.ReadJson();
        }

        private bool CheckPathExist()
        {
            return File.Exists(this.Path);
        }

        private void ReadJson()
        {
            if (this.CheckPathExist())
            {
                using (StreamReader reader = File.OpenText(this.Path))
                {
                    string json = reader.ReadToEnd();
                    this.Items = (Root)JsonConvert.DeserializeObject(json, typeof(Root)); 
                }

                if (this.Items != null)
                {
                    this.Users = this.Items.Users.ToHashSet();
                    this.Roles = this.Items.Roles.ToHashSet();
                    this.UserRoles = this.Items.UserRoles.ToHashSet();
                }
            }
        }

        private void WriteJson<T>(T json) where T : class => File.WriteAllText(this.Path, JsonConvert.SerializeObject(json));
    }
}