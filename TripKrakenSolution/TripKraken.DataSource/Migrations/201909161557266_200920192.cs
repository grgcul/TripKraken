namespace TripKraken.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _200920192 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CountryInfoID = c.Int(nullable: false),
                        RatingValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReviewDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountryInfoes", t => t.CountryInfoID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CountryInfoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "CountryInfoID", "dbo.CountryInfoes");
            DropIndex("dbo.Reviews", new[] { "CountryInfoID" });
            DropIndex("dbo.Reviews", new[] { "ApplicationUserId" });
            DropTable("dbo.Reviews");
        }
    }
}
