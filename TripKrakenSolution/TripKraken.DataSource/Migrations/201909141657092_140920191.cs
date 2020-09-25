namespace TripKraken.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _140920191 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WeatherInfoes", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.WeatherInfoes", new[] { "ApplicationUserId" });
            AddColumn("dbo.WeatherInfoes", "TempAvg", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.WeatherInfoes", "ApplicationUserId", c => c.String());
            AlterColumn("dbo.WeatherInfoes", "SunValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.WeatherInfoes", "RainValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.WeatherInfoes", "HumidityValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WeatherInfoes", "HumidityValue", c => c.Double(nullable: false));
            AlterColumn("dbo.WeatherInfoes", "RainValue", c => c.Double(nullable: false));
            AlterColumn("dbo.WeatherInfoes", "SunValue", c => c.Double(nullable: false));
            AlterColumn("dbo.WeatherInfoes", "ApplicationUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.WeatherInfoes", "TempAvg");
            CreateIndex("dbo.WeatherInfoes", "ApplicationUserId");
            AddForeignKey("dbo.WeatherInfoes", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
