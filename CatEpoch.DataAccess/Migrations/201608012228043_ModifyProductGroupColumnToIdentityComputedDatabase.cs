namespace CatEpoch.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProductGroupColumnToIdentityComputedDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductTrees", "GroupId", "dbo.ProductGroups");
            DropPrimaryKey("dbo.ProductGroups");
            AlterColumn("dbo.ProductGroups", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProductGroups", "Id");
            AddForeignKey("dbo.Products", "GroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductTrees", "GroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTrees", "GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.Products", "GroupId", "dbo.ProductGroups");
            DropPrimaryKey("dbo.ProductGroups");
            AlterColumn("dbo.ProductGroups", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductGroups", "Id");
            AddForeignKey("dbo.ProductTrees", "GroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "GroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
        }
    }
}
