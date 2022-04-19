using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<Logs> Get()
        {
            return this.context.logs.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Logs Get(int id)
        {
            return this.context.logs.Find(id);
        }

        [HttpPost]
        public Logs Create(Logs log)
        {
            this.context.logs.Add(log);
            this.context.SaveChanges();

            return log;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, Logs log)
        {
            Logs db = this.context.logs.Find(id);
            db.userID = log.userID;
            db.configID = log.configID;
            db.status = log.status;
            db.details = log.details;
            db.date = log.date;
            db.alreadySent = log.alreadySent;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Logs log = this.context.logs.Find(id);
            this.context.logs.Remove(log);
            this.context.SaveChanges();
        }
    }
}
