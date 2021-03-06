using Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data
{
    public class Context : IdentityDbContext<User>
    {
     

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<User> Users { get; set; }
        //public object File { get; set; }
        public DbSet<GroupUser>GroupUser{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Folder>();
            modelBuilder.Entity<File>();
            modelBuilder.Entity<Group>();
            modelBuilder.Entity<GroupUser>().HasKey(gu => new { gu.GroupId, gu.Id });



        }
    }
}
