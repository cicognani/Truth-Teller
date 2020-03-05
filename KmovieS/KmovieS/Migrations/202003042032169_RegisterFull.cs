namespace KmovieS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisterFull : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Company", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Company");
        }
    }
}
