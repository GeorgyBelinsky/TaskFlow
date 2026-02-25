using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;
using Task = TaskFlow.Domain.Entities.Task;

namespace TaskFlow.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<User> Users => Set<User>();
        public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(builder =>
            {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.HasMany(x => x.Tasks)
                    .WithOne(x => x.Project)
                    .HasForeignKey(x => x.ProjectId);
            });

            modelBuilder.Entity<ProjectMember>()
        .HasKey(pm => new { pm.ProjectId, pm.UserId });

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(pm => pm.UserId);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.Members)
                .HasForeignKey(pm => pm.ProjectId);
        }

    }
}
