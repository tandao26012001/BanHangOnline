namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "Color");
        }
    }
}
