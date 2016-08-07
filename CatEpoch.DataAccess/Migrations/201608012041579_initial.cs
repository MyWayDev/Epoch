namespace CatEpoch.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        ProductName = c.String(),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ProductTrees",
                c => new
                    {
                        OrgNode = c.HierarchyId(nullable: false),
                        OrgLevel = c.Int(nullable: false),
                        TreeName = c.String(),
                        ParentId = c.Int(),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgNode)
                .ForeignKey("dbo.ProductGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTrees", "GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.Products", "GroupId", "dbo.ProductGroups");
            DropIndex("dbo.ProductTrees", new[] { "GroupId" });
            DropIndex("dbo.Products", new[] { "GroupId" });
            DropTable("dbo.ProductTrees");
            DropTable("dbo.Products");
            DropTable("dbo.ProductGroups");
        }
    }
}
