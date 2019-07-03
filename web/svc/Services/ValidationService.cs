using src.DataAccess;
using System.Linq;

namespace src.Services 
{
    public interface IValidationService
    {
        bool IfEmailExist(string email);
    }

    public class ValidationService : IValidationService
    {
        private readonly DbContext _db;

        public ValidationService(DbContext db)
        {
            this._db = db;
        }

        public bool IfEmailExist(string email) => this._db.Users.Where(u => u.Email == email).FirstOrDefault() != null ? false : true;
    }
}