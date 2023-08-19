using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DTO.AspNetUsers;
using ToDoList.DTO.UsersApp;


namespace ToDoList.Infraestructure
{
    public class AppDBContext : IdentityDbContext
    {
        public const string DBO_SCHEMA = "dbo";

        public AppDBContext(DbContextOptions options) : base(options) { 
        }        
         
        public DbSet<UsersApp> UsersApp { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    } 
}
