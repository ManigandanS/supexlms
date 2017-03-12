using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Emails;
using Lms.Domain.Models.Plans;
using Lms.Domain.Models.Contents;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Repositories
{    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

    }
}
