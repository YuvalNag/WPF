namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Currencies1", "IssuesCountry_Id", "dbo.Countries");
            DropIndex("dbo.Currencies1", new[] { "IssuesCountry_Id" });
            AddColumn("dbo.Currencies1", "Direction", c => c.String());
            AddColumn("dbo.Currencies1", "IssuedCountryCode", c => c.String());
            AddColumn("dbo.Currencies1", "IssuedCountryName", c => c.String());
            DropColumn("dbo.Currencies1", "IssuesCountry_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Currencies1", "IssuesCountry_Id", c => c.Int());
            DropColumn("dbo.Currencies1", "IssuedCountryName");
            DropColumn("dbo.Currencies1", "IssuedCountryCode");
            DropColumn("dbo.Currencies1", "Direction");
            CreateIndex("dbo.Currencies1", "IssuesCountry_Id");
            AddForeignKey("dbo.Currencies1", "IssuesCountry_Id", "dbo.Countries", "Id");
        }
    }
}
