﻿using Prices.BLL.Repository_Interfaces;
using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace Prices.Controllers
{
    public class UsersController : ApiController
    {
        IUserRepository repo;
        public UsersController(IUserRepository ir) => repo = ir;

        // GET api/<controller>
        public IEnumerable<User> Get()
        {
            return repo.GetAllUsers();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            User user = new User()
            {
                User_id = "admin@admin.com",
                First_name = "admin",
                Last_name = "admin",
                Password = "admin",
                Birthdate = new DateTime(2000, 01, 01),
                Gender = true,
                State = "israel",
                City = "ashdot yaacov ichud",
            };
            repo.AddUser(user);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        // LOGIN
        [HttpPost]
        [Route("api/users/Login")]
        public User Login([FromBody]User user)
        {
            return repo.Login(user.User_id, user.Password);
        }
        //SIGNUP
        [HttpPost]
        [Route("api/users/SignUp")]
        public User SignUp([FromBody]User newUser)
        {
            repo.SignUp(newUser);
            return repo.Login(newUser.User_id, newUser.Password);

        }[HttpPost]
        [Route("api/users/UpdateUser")]
        public string UpdateUser([FromBody]User user2Update)
        {
            repo.UpdateUser(user2Update);
            return user2Update.Password;
        }
    }
}