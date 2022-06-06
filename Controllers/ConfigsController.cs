using CloneClownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public List<Configs> Get(string name)
        {
             IEnumerable<Configs> result = this.context.configs;

            if (name != null)
                result = result.Where(x => x.configName == name);

            return result.ToList();
        }
        [HttpGet]
        [Route("dashboard/config-count")]
        public int GetConfigCount()
        {
            return this.context.configs.Count();
        }
        [HttpGet]
        [Route("dashboard/average-last-backup")]
        public string GetConfigAverageDateTime()
        {
            double count = this.context.configs.Count();
            List<DateTime> dates = new List<DateTime>();
            this.context.configs.ToList().ForEach(a => dates.Add(a.last_used));
            double temp = 0d;
            for (int i = 0; i < count; i++)
            {
                temp += dates[i].Ticks / count;
            }
            DateTime tempdate = new DateTime((long)temp);
            TimeSpan result = DateTime.Now - tempdate;
            return $"{result.Days} Days and {result.Hours} Hours";
        }

        [HttpGet]
        [Route("dashboard/avg-configs")]
        public float GetAverageConfigs()
        {
            int countCnfg = 0;
            this.context.users.ToList().ForEach(a => countCnfg += a.configs.Count);

            return (float)countCnfg / (float)this.context.users.Count();
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
            db.configName = config.configName;
            db.schedule = config.schedule;
            db.last_used = config.last_used;
            db.type = config.type;
            db.backupCount = config.backupCount;
            db.packageCount = config.packageCount;
            db.isZIP = config.isZIP;

            this.context.sourceF.ToList().RemoveAll(a => a.configID == config.id);
            db.sources = config.sources;

            this.context.destF.ToList().RemoveAll(a => a.configID == config.id);
            db.dests = config.dests;

            this.context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Configs config = this.context.configs.Find(id);
            this.context.sourceF.ToList().RemoveAll(a => a.configID == config.id);

            this.context.destF.ToList().RemoveAll(a => a.configID == config.id);
            this.context.configs.Remove(config);
            this.context.users.ToList().Where(a => a.id == id).ToList().ForEach(a => this.context.users.Remove(a));
            this.context.ConfigsUsers.ToList().Where(a => a.configID == id).ToList().ForEach(a => this.context.ConfigsUsers.Remove(a));
            this.context.SaveChanges();
        }
    }
}