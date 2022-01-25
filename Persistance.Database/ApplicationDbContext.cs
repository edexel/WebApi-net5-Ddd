using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Database.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Database
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Salary> Salary { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }
       


        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new SalaryConfiguration(modelBuilder.Entity<Salary>());
        }
    }
}
