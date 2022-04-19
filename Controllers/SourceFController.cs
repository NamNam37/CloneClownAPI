using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceFController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<SourceF> Get()
        {
            return this.context.sourceF.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public SourceF Get(int id)
        {
            return this.context.sourceF.Find(id);
        }

        [HttpPost]
        public SourceF Create(SourceF sourceF)
        {
            this.context.sourceF.Add(sourceF);
            this.context.SaveChanges();

            return sourceF;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, SourceF sourceF)
        {
            SourceF db = this.context.sourceF.Find(id);
            db.configID = sourceF.configID;
            db.path = sourceF.path;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            SourceF sourceF = this.context.sourceF.Find(id);
            this.context.sourceF.Remove(sourceF);
            this.context.SaveChanges();
        }
    }
}
