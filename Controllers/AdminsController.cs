using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<Admins> Get()
        {
            return this.context.admins.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Admins Get(int id)
        {
            return this.context.admins.Find(id);
        }

        [HttpPost]
        public Admins Create(Admins admin)
        {
            this.context.admins.Add(admin);
            this.context.SaveChanges();

            return admin;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, Admins admin)
        {
            Admins db = this.context.admins.Find(id);
            db.schedule = admin.schedule;
            db.username = admin.username;
            db.password = admin.password;
            db.pfp = admin.pfp;
            db.email = admin.email;
            db.errors = admin.errors;
            db.successes = admin.successes;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Admins admin = this.context.admins.Find(id);
            this.context.admins.Remove(admin);
            this.context.SaveChanges();
        }
    }
}
