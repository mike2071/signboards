using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class SignBoardsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SignBoardsContext() : base("name=SignBoardsContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<SignBoards.Models.SignBoard> SignBoards { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.CompanyAddress> CompanyAddresses { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.SignBoardAddress> SignBoardAddresses { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.Contractor> Contractors { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.ContractorAddress> ContractorAddresses { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.SignBoardBid> SignBoardBids { get; set; }

        public System.Data.Entity.DbSet<SignBoards.Models.ContactUs> ContactUs { get; set; }
    }
}
