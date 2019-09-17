namespace TripKraken.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _100920199 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrimeRateInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CrimeRateTypeID = c.Int(nullable: false),
                        CountryInfoID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountryInfoes", t => t.CountryInfoID, cascadeDelete: true)
                .ForeignKey("dbo.CrimeRateTypes", t => t.CrimeRateTypeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CrimeRateTypeID)
                .Index(t => t.CountryInfoID);
            
            CreateTable(
                "dbo.CrimeRateTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HealthCareInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CountryInfoID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InfoDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountryInfoes", t => t.CountryInfoID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CountryInfoID);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeatherInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CountryInfoID = c.Int(nullable: false),
                        MonthID = c.Int(nullable: false),
                        SunValue = c.Double(nullable: false),
                        RainValue = c.Double(nullable: false),
                        HumidityValue = c.Double(nullable: false),
                        InfoDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountryInfoes", t => t.CountryInfoID, cascadeDelete: true)
                .ForeignKey("dbo.Months", t => t.MonthID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CountryInfoID)
                .Index(t => t.MonthID);
            
            AddColumn("dbo.CountryInfoes", "PolutionRate", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherInfoes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WeatherInfoes", "MonthID", "dbo.Months");
            DropForeignKey("dbo.WeatherInfoes", "CountryInfoID", "dbo.CountryInfoes");
            DropForeignKey("dbo.HealthCareInfoes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.HealthCareInfoes", "CountryInfoID", "dbo.CountryInfoes");
            DropForeignKey("dbo.CrimeRateInfoes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CrimeRateInfoes", "CrimeRateTypeID", "dbo.CrimeRateTypes");
            DropForeignKey("dbo.CrimeRateInfoes", "CountryInfoID", "dbo.CountryInfoes");
            DropIndex("dbo.WeatherInfoes", new[] { "MonthID" });
            DropIndex("dbo.WeatherInfoes", new[] { "CountryInfoID" });
            DropIndex("dbo.WeatherInfoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.HealthCareInfoes", new[] { "CountryInfoID" });
            DropIndex("dbo.HealthCareInfoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.CrimeRateInfoes", new[] { "CountryInfoID" });
            DropIndex("dbo.CrimeRateInfoes", new[] { "CrimeRateTypeID" });
            DropIndex("dbo.CrimeRateInfoes", new[] { "ApplicationUserId" });
            DropColumn("dbo.CountryInfoes", "PolutionRate");
            DropTable("dbo.WeatherInfoes");
            DropTable("dbo.Months");
            DropTable("dbo.HealthCareInfoes");
            DropTable("dbo.CrimeRateTypes");
            DropTable("dbo.CrimeRateInfoes");
        }
    }
}
