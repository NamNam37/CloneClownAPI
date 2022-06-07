
using CloneClownAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneClownAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IScheduler scheduler;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                );
            });

            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();

            ConfigureJobs().Wait();
        }
        public async Task ConfigureJobs() 
        {
            MyContext context = new MyContext();
            List<Admins> admins = context.admins.ToList();
            foreach (Admins admin in admins)
            {

                List<string> parts = admin.schedule.Split(" ").ToList();
                if (parts[2] == "*")
                    parts[2] = "?";
                else
                    parts[4] = "?";

                string cron = string.Join(" ", parts);
                cron = "0 " + cron;

                IJobDetail job = JobBuilder.Create<MailJob>()
                    .WithIdentity($"{admin.username}_job")
                    .UsingJobData("adminId", admin.id)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity($"{admin.username}_trigger_job")
                    .StartNow()
                    .WithCronSchedule(cron)
                    .Build();

                scheduler.ScheduleJob(job, trigger).Wait();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}