using LogicBoot.Api.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicBoot.Api.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here  

            modelBuilder.Entity<User>()
                    .HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(x=>x.RoleId);
        }
    }
}
