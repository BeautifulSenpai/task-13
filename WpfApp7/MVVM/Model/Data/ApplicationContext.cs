using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7.MVVM.Model.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set;  }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set;  }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Task13DB;Trusted_Connection=True");
        }
    }
}
