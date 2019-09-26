namespace TripKraken.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31082019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostOfLivingInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                        PriceForTypeID = c.Int(nullable: false),
                        CountryInfoID = c.Int(nullable: false),
                        CurrencyID = c.Int(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InfoDescription = c.String(maxLength: 500),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountryInfoes", t => t.CountryInfoID, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID)
                .ForeignKey("dbo.PriceForTypes", t => t.PriceForTypeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.PriceForTypeID)
                .Index(t => t.CountryInfoID)
                .Index(t => t.CurrencyID)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceForTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CostOfLivingInfoes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CostOfLivingInfoes", "PriceForTypeID", "dbo.PriceForTypes");
            DropForeignKey("dbo.CostOfLivingInfoes", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.CostOfLivingInfoes", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.CostOfLivingInfoes", "CountryInfoID", "dbo.CountryInfoes");
            DropIndex("dbo.CostOfLivingInfoes", new[] { "Country_Id" });
            DropIndex("dbo.CostOfLivingInfoes", new[] { "CurrencyID" });
            DropIndex("dbo.CostOfLivingInfoes", new[] { "CountryInfoID" });
            DropIndex("dbo.CostOfLivingInfoes", new[] { "PriceForTypeID" });
            DropIndex("dbo.CostOfLivingInfoes", new[] { "ApplicationUserId" });
            DropTable("dbo.PriceForTypes");
            DropTable("dbo.Currencies");
            DropTable("dbo.CostOfLivingInfoes");
        }
    }
}
