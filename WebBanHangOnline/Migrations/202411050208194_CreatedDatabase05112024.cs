namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase05112024 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_SpecialOffers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_SpecialOffers", "IsActive");
        }
    }
}
