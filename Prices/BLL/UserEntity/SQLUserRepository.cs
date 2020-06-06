using Prices.BLL.Repository_Interfaces;
using Prices.DAL.SQLConnection;
using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prices.BLL.UserEntity
{
    public class SQLUserRepository : IUserRepository
    {
        DBServices db = new DBServices();

        public User AddUser(User user)
        {
            user.User_rank = 1000;
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public User Login(string id, string password)
        {
            //User u = (User)db.SPGetById(new User(), "SelectByUserId", id) as User;
            List<User> u = db.SPGetById(new User(), "SelectByUserId", id) as List<User>;
            if (u.Count == 1)
            {
                User user = u[0];
                return user.Password == password ? user : new User();
            }
            return new User();
        }

        public User SignUp(User newUser)
        {
            db.InsertToDB<User>(newUser);
            return newUser;
            //throw new NotImplementedException();
        }

        public User UpdateUser(string id, User user)
        {
            throw new NotImplementedException();
        }

    }
}