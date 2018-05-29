namespace Restaurant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderItemProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "NumberOfItems", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "NumberOfItems");
        }
    }
}
