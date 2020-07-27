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
            //user.User_rank = 1000;
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

        public IEnumerable<Item> GetReceipts2verify(User user)
        {
            List<Item> results = (List<Item>)db.SPGetResults(new Search<Item>()
            {
                Model = new Item(),
                Statement_Type = "verifyReceipts",
                User = user,
            });
            for (int i = 0; i < results.Count; i++)
            {
                results[i].Tags = (List<Tag>)db.SPGetById(new Tag(), "SelectByItemId", results[i].Item_id);//Add tags for each item
            }

            return results;
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
                u[0].Favorites = (List<string>)db.SPGetById("favorites", "selectUserFavorites", u[0].User_id);
                User user = u[0];
                return user.Password == password ? user : new User();
            }
            return new User();
        }

        public bool SetReceiptStatus(Receipt receipt)
        {
            db.SPUpdate(receipt);
            Receipt r = ((List<Receipt>)db.SPGetById(new Receipt(), "SelectByReceiptId", receipt.Receipt_id)).FirstOrDefault();
            if (r != null)
            {
                receipt.Receipt_rank = r.Receipt_rank;
                if (receipt.Status)
                {
                    db.SPUpdate(new User() { User_id = receipt.User_id, User_rank = receipt.Receipt_rank });
                }
            }
            return receipt.Status;
        }

        public User SignUp(User newUser)
        {
            db.InsertToDB<User>(newUser);
            return newUser;
            //throw new NotImplementedException();
        }

        public void UpdateFavorites(User user)
        {
            db.SPUpdateUserProfile(user);
        }

        public User UpdateUser(User user2Update)
        {//here
            db.SPUpdateUserProfile(user2Update);
            return user2Update;
        }
    }
}