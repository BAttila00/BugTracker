using BugTracker.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal {
    public class BugTrackerDbContext : DbContext {
        public BugTrackerDbContext() {

        }
        // ezen options-el az adatbázis-kapcsolat konfigurációs beállításait (timeout, connection string) állíthatjuk be.
        public BugTrackerDbContext(DbContextOptions options) : base(options) {

        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {


            modelBuilder.Entity<ProjectUser>(entity => {
                entity.HasKey(e => new { e.UserId, e.ProjectId });

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUser_Project");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUser_User");
            });

            modelBuilder.Entity<Comment>(entity => {

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });
        }
    }
}
