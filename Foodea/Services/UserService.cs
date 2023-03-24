﻿using Foodea.Data;
using Foodea.Models;
using Microsoft.EntityFrameworkCore;

namespace Foodea.Services{
    public interface IUserServices {
        public List<User> getAllUsers();
        public User getUserById(Guid id);
        public User getUserByEmail(string email);
        public Boolean createUser(User user);
        public string deleteUserById(Guid id);


    }
    public class UserService: IUserServices{

        private readonly FoodeaDbContext foodeaDbContext;

        public UserService(FoodeaDbContext dbContext) {
            foodeaDbContext = dbContext;
        }
        public Boolean createUser(User user) {
            user.UserId = Guid.NewGuid();
            try {
                this.foodeaDbContext.User.Add(user);
                this.foodeaDbContext.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
            
        }
        public List<User> getAllUsers() {
           return this.foodeaDbContext.User.ToList();
        }

        public User getUserById(Guid id) {
            return this.foodeaDbContext.User.Find(id);
        }

        public User getUserByEmail(string email) {
            return this.foodeaDbContext.User.FirstOrDefault(e=> e.Email == email);
        }

        public string deleteUserById(Guid id) {
            var user = this.getUserById(id);
            if (user == null) {
                return "User does not exist";
            }
            var reponse = this.foodeaDbContext.User.Remove(user);
            this.foodeaDbContext.SaveChanges();
            return "User deleted successfully";
        }
    }
}
