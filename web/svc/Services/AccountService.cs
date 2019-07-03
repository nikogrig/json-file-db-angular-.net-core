using System.Linq;
using System.Threading.Tasks;
using src.ViewModels;
using src.DataAccess;
using src.DataAccess.DTO;
using src.ServiceModel;
using System;
using System.Collections.Generic;

namespace src.Services
{
    public interface IAccountService
    {
        UserViewModel Register(UserRegisterViewModel model);

        UserViewModel Login(UserLoginViewModel model);
    }

    public class AccountService : IAccountService
    {
        private readonly DbContext _db;
        public AccountService(DbContext db)
        {
            this._db = db;
        }

        public UserViewModel Login(UserLoginViewModel model)
        {
            UserServiceModel  user = this.GetUserData(model);

            return user == null ? null : this.ConvertToUserViewModel(user);        
        }

        public UserViewModel Register(UserRegisterViewModel model)
        {
            if (this.MatchEmail(model.Email))
                return null;

            User userDto = new User
            {
                Id = this._db.GenerateId,
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Telephone = model.Telephone
            };

            UserServiceModel result = this._db.Create(userDto);

            return this.ConvertToUserViewModel(result);
        }

        private UserServiceModel GetUserData(UserLoginViewModel model)
        {
            User user = this._db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if(user == null)
                return null;

            int roleId = this._db.UserRoles.Where(r => r.USerId == user.Id).Select(r => r.RoleId).FirstOrDefault();

            return new UserServiceModel 
            {
                User = user,
                Role = this._db.Roles.Where(r => r.Id == roleId).Select(r => r.AccountRole).FirstOrDefault()
            };
        }

        private bool MatchEmail(string email)
        {
            User user = this._db.Users.Where(u => u.Email == email).FirstOrDefault();
            return user != null ? true : false;
        }

        private UserViewModel ConvertToUserViewModel(UserServiceModel model) 
        {
            return new UserViewModel
            {
                Id = model.User.Id,
                UserName = model.User.UserName,
                Email = model.User.Email,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                Telephone = model.User.Telephone,
                Role = model.Role
            };
        }

    }
}