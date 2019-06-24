using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamTaskManager.Models;

namespace TeamTaskManager.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<MTeam> Team { get; set; }
        public DbSet<MUser> User { get; set; }

        public DbSet<MProject> project { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MTeam>()
                 .HasMany<MUser>(t => t.user)
                 .WithOne(u => u.Team)
                 .HasForeignKey(f => f.teamIdFK);
            modelBuilder.Entity<MTeam>()
                .HasOne<MProject>(t => t.project)
                .WithOne(t => t.Team)
                .HasForeignKey<MProject>(f => f.teamIdFK);
        }
    }
}
