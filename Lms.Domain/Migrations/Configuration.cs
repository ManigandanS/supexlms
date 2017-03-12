namespace Lms.Domain.Migrations
{
    using Lms.Domain.Models.Commons;
    using Lms.Domain.Models.Companies;
    using Lms.Domain.Models.Plans;
    using Lms.Domain.Models.Users;
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Lms.Domain.Repositories.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CodeGenerator = new MySqlMigrationCodeGenerator();
        }

        protected override void Seed(Lms.Domain.Repositories.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            try
            {
                context.Configurations.AddOrUpdate(
                    p => p.Title,
                    new Domain.Models.Commons.Configuration() { Title = "Office 365 Sign In", Description = "This configuration allows your staffs to single sign on using Office 365.", Code = Domain.Models.Commons.Configuration.OFFICE365_CODE }
                );
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {
                var sb = new StringBuilder();
                foreach (var failure in dbex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), dbex
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                context.Currencies.AddOrUpdate(
                    p => p.Code,
                    new Currency() { Code = "CAD", Name = "Canadian Dollar" },
                    new Currency() { Code = "USD", Name = "US Dollar" }
                );
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {
                var sb = new StringBuilder();
                foreach (var failure in dbex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), dbex
                ); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            try
            {
                context.Plans.AddOrUpdate(
                    p => p.Name,
                    new Plan() { CurrencyId = context.Currencies.First().Id, Description = "Basic Plan", MaxStorage = long.MaxValue, MaxUser = 200, Name = "Basic Plan", Price = 100 },
                    new Plan() { CurrencyId = context.Currencies.First().Id, Description = "Advanced Plan", MaxStorage = long.MaxValue, MaxUser = 1000, Name = "Advanced Plan", Price = 500 }
                );
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {
                var sb = new StringBuilder();
                foreach (var failure in dbex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), dbex
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
