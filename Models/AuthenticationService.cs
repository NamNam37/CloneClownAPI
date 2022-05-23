using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneClownAPI.Models
{
    public class AuthenticationService
    {
        const string SECRET = "$#@gr5e4a!$^&ggfar9e@$";

        private MyContext context = new MyContext();

        public string Authenticate(Credentials credentials)
        {
            Admins admin = this.context.admins.Where(x => x.username == credentials.Login && x.password == credentials.Password).FirstOrDefault();

            if (admin == null)
                throw new Exception("invalid admin");

            return JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                      .WithSecret(SECRET)
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(2).ToUnixTimeSeconds())
                      .AddClaim("admin_id", admin.id)
                      .Encode();
        }

        public bool VerifyToken(string token)
        {
            try
            {
                string json = JwtBuilder.Create()
                             .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                             .WithSecret(SECRET)
                             .MustVerifySignature()
                             .Decode(token);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
