using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<Configs> Get()
        {
            return this.context.configs.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Configs Get(int id)
        {
            return this.context.configs.Find(id);
        }

        [HttpPost]
        public Configs Create(Configs config)
        {
            this.context.configs.Add(config);
            this.context.SaveChanges();

            return config;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, Configs config)
        {
            Configs db = this.context.configs.Find(id);
            db.id = config.id;
            db.schedule = config.schedule;
            db.last_used = config.last_used;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Configs config = this.context.configs.Find(id);
            this.context.configs.Remove(config);
            this.context.SaveChanges();
        }
    }
}
