﻿namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ProductImage", "ImageUrl", c => c.String());
            DropColumn("dbo.tb_ProductImage", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductImage", "Image", c => c.String());
            DropColumn("dbo.tb_ProductImage", "ImageUrl");
        }
    }
}
