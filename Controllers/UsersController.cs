using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<Users> Get()
        {
            return this.context.users.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Users Get(int id)
        {
            return this.context.users.Find(id);
        }

        [HttpPost]
        public Users Create(Users user)
        {
            this.context.users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, Users user)
        {

            //this.context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            Users db = this.context.users.Find(id);
            
            db.username = user.username;
            db.IP = user.IP;
            db.online = user.online;
            db.last_backup = user.last_backup;
            db.logs = user.logs;
            db.configsUsers.ToList().ForEach(x => this.context.ConfigsUsers.Remove(x));
            db.configsUsers = user.configs.Select(x => new ConfigsUsers() { configID = x.id, userID = id }).ToList();

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Users user = this.context.users.Find(id);
            this.context.users.Remove(user);
            this.context.SaveChanges();
        }
    }
}
