using Microsoft.EntityFrameworkCore;

namespace CloneClownAPI.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Configs> configs { get; set; }
        public DbSet<Admins> admins { get; set; }
        public DbSet<Logs> logs { get; set; }
        public DbSet<UC> UC { get; set; }
        public DbSet<SourceF> sourceF { get; set; }
        public DbSet<DestF> destF { get; set; }
        public DbSet<FTP> ftp { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3c1_pychadavid_db1;user=pychadavid;password=123456;SslMode=none");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DestF>()
                .HasOne(a => a.FTP)
                .WithOne(a => a.DestF)
                .HasForeignKey<FTP>(a => a.destID);
        }
    }
}
