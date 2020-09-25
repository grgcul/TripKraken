namespace TripKraken.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migratio29082019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CountryID = c.Int(nullable: false),
                        WiredInternetSpeed = c.String(maxLength: 50),
                        MobileInternetSpeed = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountryInfoes", "CountryID", "dbo.Countries");
            DropIndex("dbo.CountryInfoes", new[] { "CountryID" });
            DropTable("dbo.CountryInfoes");
        }
    }
}
