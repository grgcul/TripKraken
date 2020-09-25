namespace TripKraken.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17092019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountryInfoes", "LandArea", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CountryInfoes", "Population", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CountryInfoes", "Density", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountryInfoes", "Density");
            DropColumn("dbo.CountryInfoes", "Population");
            DropColumn("dbo.CountryInfoes", "LandArea");
        }
    }
}
