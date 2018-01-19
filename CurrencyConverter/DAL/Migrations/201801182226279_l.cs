namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class l : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Currencies1", "Direction", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Currencies1", "Direction");
        }
    }
}
