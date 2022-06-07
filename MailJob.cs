using CloneClownAPI.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloneClownAPI
{
    public class MailJob : IJob
    {
        private int adminId;
        public async Task Execute(IJobExecutionContext context)
        {
            MyContext myContext = new MyContext();

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            adminId = dataMap.GetIntValue("adminId");

            Admins admin = myContext.admins.ToList().Find(a => adminId == a.id );
            List<Logs> logs = myContext.logs.Where(a => !a.alreadySent).ToList();

            

            string body = $"Hello {admin.username}.\n";
            body += $"Succesful Logs: {logs.Where(a => a.status).Count()}/{logs.Count}.\n";
            body += $"Failed Logs: {logs.Where(a => !a.status).Count()}/{logs.Count}.\n";
            body += $"\n______________________________________________\n";
            logs.Where(a => a.config != null).ToList()
                .ForEach(a => body += $"{a.user.username} has used configuration {a.config.configName}: status={a.status}, details={a.details}.\n");
            logs.Where(a => a.config == null).ToList()
                .ForEach(a => body += $"{a.user.username}: status={a.status}, details={a.details}.\n");

            try
            {

                    /*SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("cloneclown.backups@gmail.coma", "Ab123456+"),
                        EnableSsl = true,
                    };
                    smtpClient.Send("cloneclown.backups@gmail.com", admin.email, $"Report from Clone Clown", body);*/

                    using (var message = new MailMessage("cloneclown.backups@gmail.com", "cloneclown.backups@gmail.com"))
                    {
                        message.Subject = $"Report from Clone Clown";
                        message.Body = body;
                        using (SmtpClient client = new SmtpClient
                        {
                            EnableSsl = true,
                            Host = "smtp.gmail.com",
                            Port = 587,
                            Credentials = new NetworkCredential("cloneclown.backups@gmail.com", "Ab123456+")
                        })
                        {
                            client.Send(message);
                        }
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
