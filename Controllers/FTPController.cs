using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CloneClownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FTPController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public List<FTP> Get()
        {
            return this.context.ftp.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public FTP Get(int id)
        {
            return this.context.ftp.Find(id);
        }

        [HttpPost]
        public FTP Create(FTP ftp)
        {
            this.context.ftp.Add(ftp);
            this.context.SaveChanges();

            return ftp;
        }

        [HttpPut]
        [Route("{id}")]
        public void Update(int id, FTP ftp)
        {
            FTP db = this.context.ftp.Find(id);
            db.destID = ftp.destID;
            db.hostname = ftp.hostname;
            db.login = ftp.login;
            db.password = ftp.password;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            FTP ftp = this.context.ftp.Find(id);
            this.context.ftp.Remove(ftp);
            this.context.SaveChanges();
        }
    }
}
