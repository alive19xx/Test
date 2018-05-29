namespace Restaurant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropOrderMenuItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.MenuItems", new[] { "Order_Id" });
            DropColumn("dbo.MenuItems", "Order_Id");
            DropColumn("dbo.Orders", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MenuItems", "Order_Id", c => c.Int());
            CreateIndex("dbo.MenuItems", "Order_Id");
            AddForeignKey("dbo.MenuItems", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
