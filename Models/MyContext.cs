using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CloneClownAPI.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public IEnumerable<object> Users { get; internal set; }
        public DbSet<Configs> configs { get; set; }
        public DbSet<Admins> admins { get; set; }
        public DbSet<Logs> logs { get; set; }
        public DbSet<ConfigsUsers> ConfigsUsers { get; set; }
        public DbSet<SourceF> sourceF { get; set; }
        public DbSet<DestF> destF { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3c1_pychadavid_db1;user=pychadavid;password=123456;SslMode=none");
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
                .HasMany(a => a.logs)
                .WithOne(a => a.user)
                .HasForeignKey(a => a.userID);

            modelBuilder.Entity<Configs>()
                .HasMany(a => a.logs)
                .WithOne(a => a.config)
                .HasForeignKey(a => a.configID);

            modelBuilder.Entity<Configs>()
                .HasMany(a => a.sources)
                .WithOne(a => a.config)
                .HasForeignKey(a => a.configID);

            modelBuilder.Entity<Configs>()
                .HasMany(a => a.dests)
                .WithOne(a => a.config)
                .HasForeignKey(a => a.configID);

            modelBuilder.Entity<Configs>()
            .HasMany(p => p.users)
            .WithMany(p => p.configs)
            .UsingEntity<ConfigsUsers>(
                j => j
                    .HasOne(pt => pt.user)
                    .WithMany(t => t.configsUsers)
                    .HasForeignKey(pt => pt.userID),
                j => j
                    .HasOne(pt => pt.config)
                    .WithMany(p => p.configsUsers)
                    .HasForeignKey(pt => pt.configID),
                j =>
                {
                    j.HasKey(t => new { t.configID, t.userID });
                });
        }
    }
}
