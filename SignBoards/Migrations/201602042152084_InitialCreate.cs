namespace SignBoards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ContactId = c.Guid(nullable: false),
                        CompanyAddressId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyAddresses", t => t.CompanyAddressId)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .Index(t => t.ContactId)
                .Index(t => t.CompanyAddressId);
            
            CreateTable(
                "dbo.CompanyAddresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Postcode = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        TelephoneNumber = c.Int(nullable: false),
                        MobileNumber = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractorAddresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Postcode = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContactId = c.Guid(nullable: false),
                        ContractorAddressId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.ContractorAddresses", t => t.ContractorAddressId)
                .Index(t => t.ContactId)
                .Index(t => t.ContractorAddressId);
            
            CreateTable(
                "dbo.SignBoardAddresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Postcode = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SignBoardBids",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SignBoardId = c.Guid(nullable: false),
                        ContractorId = c.Guid(nullable: false),
                        BidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidDate = c.DateTime(nullable: false),
                        IsBidWinner = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.ContractorId)
                .ForeignKey("dbo.SignBoards", t => t.SignBoardId)
                .Index(t => t.SignBoardId)
                .Index(t => t.ContractorId);
            
            CreateTable(
                "dbo.SignBoards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        SignBoardAddressId = c.Guid(nullable: false),
                        FittingInstructions = c.String(),
                        FittingCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FittingType = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.SignBoardAddresses", t => t.SignBoardAddressId)
                .Index(t => t.CompanyId)
                .Index(t => t.SignBoardAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignBoardBids", "SignBoardId", "dbo.SignBoards");
            DropForeignKey("dbo.SignBoards", "SignBoardAddressId", "dbo.SignBoardAddresses");
            DropForeignKey("dbo.SignBoards", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.SignBoardBids", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.Contractors", "ContractorAddressId", "dbo.ContractorAddresses");
            DropForeignKey("dbo.Contractors", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Companies", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Companies", "CompanyAddressId", "dbo.CompanyAddresses");
            DropIndex("dbo.SignBoards", new[] { "SignBoardAddressId" });
            DropIndex("dbo.SignBoards", new[] { "CompanyId" });
            DropIndex("dbo.SignBoardBids", new[] { "ContractorId" });
            DropIndex("dbo.SignBoardBids", new[] { "SignBoardId" });
            DropIndex("dbo.Contractors", new[] { "ContractorAddressId" });
            DropIndex("dbo.Contractors", new[] { "ContactId" });
            DropIndex("dbo.Companies", new[] { "CompanyAddressId" });
            DropIndex("dbo.Companies", new[] { "ContactId" });
            DropTable("dbo.SignBoards");
            DropTable("dbo.SignBoardBids");
            DropTable("dbo.SignBoardAddresses");
            DropTable("dbo.Contractors");
            DropTable("dbo.ContractorAddresses");
            DropTable("dbo.Contacts");
            DropTable("dbo.CompanyAddresses");
            DropTable("dbo.Companies");
        }
    }
}
