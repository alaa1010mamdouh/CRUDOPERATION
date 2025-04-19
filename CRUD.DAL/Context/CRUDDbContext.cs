using CRUD.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL.Context
{
    public class CRUDDbContext:IdentityDbContext<ApplicationUser>
    {

        public CRUDDbContext(DbContextOptions<CRUDDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CRUDMVC; Trusted_Connection = True; MultipleActiveResultSets=true");
        //}

        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }

       
    }
}
