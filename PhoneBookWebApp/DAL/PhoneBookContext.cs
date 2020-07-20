using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using PhoneBookWebApp.Models;

namespace PhoneBookWebApp.DAL
{
    public class PhoneBookContext :DbContext
    {
        public PhoneBookContext() : base("PhoneBookContext")
        {



        }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Address>().HasRequired(x => x.People).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<State>().HasRequired(x => x.Country).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<City>().HasRequired(x => x.State).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>().HasRequired(x => x.Country).WithOptional().WillCascadeOnDelete(false);
            modelBuilder.Entity<Address>().HasRequired(x => x.State).WithOptional().WillCascadeOnDelete(false);
            modelBuilder.Entity<Address>().HasRequired(x => x.City).WithOptional().WillCascadeOnDelete(false);
        }
    }
}