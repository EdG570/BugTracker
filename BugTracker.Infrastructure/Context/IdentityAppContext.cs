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

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        
    }
}
