namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixth : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Distributions", "UserId");
            AddForeignKey("dbo.Distributions", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Distributions", "UserId", "dbo.Users");
            DropIndex("dbo.Distributions", new[] { "UserId" });
        }
    }
}
