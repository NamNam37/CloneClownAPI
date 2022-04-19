using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestFController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<DestF> Get()
        {
            return this.context.destF.ToList();
        }


        [HttpGet]
        [Route("{id}")]
        public DestF Get(int id)
        {
            return this.context.destF.Find(id);
        }

        [HttpPost]
        public DestF Create(DestF destF)
        {
            this.context.destF.Add(destF);
            this.context.SaveChanges();

            return destF;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, DestF destF)
        {
            DestF db = this.context.destF.Find(id);
            db.configID = destF.configID;
            db.path = destF.path;
            db.type = destF.type;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            DestF destF = this.context.destF.Find(id);
            this.context.destF.Remove(destF);
            this.context.SaveChanges();
        }
    }
}
