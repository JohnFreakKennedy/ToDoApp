using Microsoft.EntityFrameworkCore;
using ToDoAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoAppDAL
{
    public class ToDoDbContext:DbContext
    {
        public ToDoDbContext()
        {

        }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DANKOVPC;Initial Catalog=ToDoAppDB;Integrated Security=True");
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<>
        }
        */
    }
}
