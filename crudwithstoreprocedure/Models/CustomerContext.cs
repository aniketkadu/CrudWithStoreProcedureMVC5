using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace crudwithstoreprocedure.Models
{
    public class CustomerContext:DbContext
    {

        public CustomerContext():base("myConnectionString")
        {

        }

        //public DbSet<Customer> StorCustomers { get; set; }
        public DbSet<State4> State4 { get; set; }
        public DbSet<City4> City4 { get; set; }
        //public DbSet<Address> StoreAddress { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Customer>().MapToStoredProcedures();

            modelBuilder.Entity<CustomerViewModel>().Map(e =>
            {
                e.Properties(p => new { p.Name, p.Email });
                e.ToTable("CustomerA");
            }).Map(e =>
            {
                e.Properties(p => new {  p.address });
                e.ToTable("AddressA");
            }).Map(e =>
            {
                e.Properties(p => new { p.State });
                e.ToTable("StateA");
            }).Map(e =>
            {
                e.Properties(p => new { p.City });
                e.ToTable("CityA");
            }).MapToStoredProcedures();

        }
        public System.Data.Entity.DbSet<CustomerViewModel> CustomerViewModels { get; set; }
    }
}