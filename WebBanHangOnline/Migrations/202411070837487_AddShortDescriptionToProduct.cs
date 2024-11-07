namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortDescriptionToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "ShortDescription");
        }
    }
}
