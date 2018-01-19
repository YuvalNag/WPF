namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies1",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Magnitude = c.Single(nullable: false),
                        Value = c.Single(nullable: false),
                        IssuesCountry_Id = c.Int(),
                        Currencies_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Countries", t => t.IssuesCountry_Id)
                .ForeignKey("dbo.Currencies", t => t.Currencies_id)
                .Index(t => t.IssuesCountry_Id)
                .Index(t => t.Currencies_id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Currencies1", "Currencies_id", "dbo.Currencies");
            DropForeignKey("dbo.Currencies1", "IssuesCountry_Id", "dbo.Countries");
            DropIndex("dbo.Currencies1", new[] { "Currencies_id" });
            DropIndex("dbo.Currencies1", new[] { "IssuesCountry_Id" });
            DropTable("dbo.Currencies");
            DropTable("dbo.Currencies1");
            DropTable("dbo.Countries");
        }
    }
}
