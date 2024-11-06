namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase30101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Slides", "Description", c => c.String());
            AddColumn("dbo.tb_Slides", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Slides", "Detail");
            DropColumn("dbo.tb_Slides", "Description");
        }
    }
}
