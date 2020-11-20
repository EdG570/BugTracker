using BugTracker.Core.Models;
using BugTracker.Core.Models.Enums;
using Microsoft.AspNetCore.Identity;
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

            /* ------------------------------- SEEDS -------------------------------- */

            //// Seed Users
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            PasswordHasher<AppUser> ph2 = new PasswordHasher<AppUser>();

            AppUser appUser = new AppUser
            {
                Id = 1,
                UserName = "user@email.com",
                NormalizedUserName = "USER@EMAIL.COM",
                Email = "user@email.com",
                NormalizedEmail = "USER@EMAIL.COM",
                FirstName = "John",
                LastName = "Smith",
                PasswordHash = ph.HashPassword(null, "Pass123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")

            };

            AppUser appUser2 = new AppUser
            {
                Id = 2,
                UserName = "user2@email.com",
                NormalizedUserName = "USER2@EMAIL.COM",
                Email = "user2@email.com",
                NormalizedEmail = "USER2@EMAIL.COM",
                FirstName = "Billy",
                LastName = "Bobson",
                PasswordHash = ph2.HashPassword(null, "Pass234!"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.Entity<AppUser>().HasData(
                appUser,
                appUser2
            );

            // Seed Projects
            builder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Description = "A web application for managing staffing",
                    Name = "Staffing App",
                    OwnerId = 1
                },
                new Project
                {
                    Id = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Description = "A web application for managing quality control",
                    Name = "Qc App",
                    OwnerId = 1
                },
                new Project
                {
                    Id = 3,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Description = "An internal web app for measuring agent performance",
                    Name = "Scorecard App",
                    OwnerId = 1
                },
                new Project
                {
                    Id = 4,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Description = "A web application for agents to monitor performance",
                    Name = "Scoreboard App",
                    OwnerId = 1
                },
                new Project
                {
                    Id = 5,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Description = "A web app for error tracking",
                    Name = "Error Tracker",
                    OwnerId = 1
                }
            );

            // Seed Tickets
            builder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    Title = "Joe Borski's team will not update",
                    Description = "I've attempted to update his team numerous times but to no avail. Can you take a look?",
                    AssignedToId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.High,
                    Status = Status.Open,
                    ProjectId = 1,
                    RequestorId = 1,
                    Type = TicketType.Bug
                },
                new Ticket
                {
                    Id = 2,
                    Title = "Susan Jones isn't showing up for this month",
                    Description = "Can you check why she isn't loading for this month?",
                    AssignedToId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.High,
                    Status = Status.Open,
                    ProjectId = 1,
                    RequestorId = 1,
                    Type = TicketType.Bug
                },
                new Ticket
                {
                    Id = 3,
                    Title = "Filter by employee last name",
                    Description = "We'd like to filter by employee last name. Is this something you guys can add to the app?",
                    AssignedToId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.Low,
                    Status = Status.Closed,
                    ProjectId = 1,
                    RequestorId = 1,
                    Type = TicketType.Feature
                },
                new Ticket
                {
                    Id = 4,
                    Title = "The app is down!",
                    Description = "Staffing is due by close of business and noone can access the app.",
                    AssignedToId = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.Urgent,
                    Status = Status.InProgress,
                    ProjectId = 1,
                    RequestorId = 1,
                    Type = TicketType.Bug
                },
                new Ticket
                {
                    Id = 5,
                    Title = "Is this app working?",
                    Description = "I can't load the staffing app.",
                    AssignedToId = 0,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.Urgent,
                    Status = Status.Open,
                    ProjectId = 1,
                    RequestorId = 1,
                    Type = TicketType.Bug
                },
                new Ticket
                {
                    Id = 6,
                    Title = "Only 5 records for today",
                    Description = "There are only 5 records to qc for today. Is this correct?",
                    AssignedToId = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.High,
                    Status = Status.InProgress,
                    ProjectId = 2,
                    RequestorId = 2,
                    Type = TicketType.Bug
                },
                new Ticket
                {
                    Id = 7,
                    Title = "Add a check for total records",
                    Description = "We'll need to add a check to see if all the records are being pulled in from the call center.",
                    AssignedToId = 0,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    Priority = Priority.Medium,
                    Status = Status.Open,
                    ProjectId = 2,
                    RequestorId = 2,
                    Type = TicketType.Task
                }
            );

            builder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    TicketId = 1,
                    Description = "I'm troubleshooting this now",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "John Smith",
                    CreatorId = 1
                },
                new Comment
                {
                    Id = 2,
                    TicketId = 1,
                    Description = "Ok let me know what you find!",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Billy Bobson",
                    CreatorId = 2
                }
            );

            builder.Entity<UserProject>().HasData(
                new UserProject
                {
                    AppUserId = 1,
                    ProjectId = 1
                },
                new UserProject
                {
                    AppUserId = 1,
                    ProjectId = 2
                },
                new UserProject
                {
                    AppUserId = 1,
                    ProjectId = 3
                },
                new UserProject
                {
                    AppUserId = 1,
                    ProjectId = 4
                },
                new UserProject
                {
                    AppUserId = 1,
                    ProjectId = 5
                }
            );

        }

    }
}
