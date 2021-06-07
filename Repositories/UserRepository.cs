using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(
                new User
                {
                    Id = 1,
                    Username = "ester",
                    Password = "456",
                    Role = "manager"
                });
            users.Add(
                new User
                {
                    Id = 1,
                    Username = "joao",
                    Password = "123",
                    Role = "employee"
                });

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
