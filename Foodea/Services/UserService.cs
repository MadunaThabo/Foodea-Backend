using Foodea.Data;
using Foodea.Models;
using Microsoft.EntityFrameworkCore;

namespace Foodea.Services{
    public interface IUserServices {
        public List<User> GetAllUsers();
        public User GetUserById(Guid id);
        public User GetUserByEmail(string email);


    }
    public class UserService: IUserServices{

        private readonly FoodeaDbContext DbContext;

        public UserService(FoodeaDbContext dbContext) {
            DbContext = dbContext;
        }

        public List<User> GetAllUsers() {
           return this.DbContext.User.ToList();
        }

        public User GetUserById(Guid id) {
            return this.DbContext.User.Find(id);
        }

        public User GetUserByEmail(string email) {
            return this.DbContext.User.FirstOrDefault(e=> e.Email == email);
        }

    }
}
