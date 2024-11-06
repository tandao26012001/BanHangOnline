namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase0511 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_SpecialOffers", "TitleProductCategory", c => c.String(maxLength: 150));
            AddColumn("dbo.tb_SpecialOffers", "ProductSaleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_SpecialOffers", "ProductSaleName");
            DropColumn("dbo.tb_SpecialOffers", "TitleProductCategory");
        }
    }
}
