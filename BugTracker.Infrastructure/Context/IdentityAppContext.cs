using BugTracker.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Infrastructure.Context
{
    public class IdentityAppContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {

        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProject>().HasKey(tu => new { tu.ProjectId, tu.AppUserId });

        }

    }
}
