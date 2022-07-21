using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-2993R3B\\SQLEXPRESS; database=REHBER; integrated security=true;");
        }


        public DbSet<kisiler> kisilers { get; set; }

        public DbSet<sehirler> sehirlers { get; set; }
        
    }
}
