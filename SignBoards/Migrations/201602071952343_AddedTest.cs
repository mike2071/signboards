namespace SignBoards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SignBoards", "FittingTypeId", c => c.Guid(nullable: false));
            DropColumn("dbo.SignBoards", "FittingType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SignBoards", "FittingType", c => c.Guid(nullable: false));
            DropColumn("dbo.SignBoards", "FittingTypeId");
        }
    }
}
