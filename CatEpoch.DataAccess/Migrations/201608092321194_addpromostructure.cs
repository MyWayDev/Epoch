namespace CatEpoch.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpromostructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Periods",
                c => new
                    {
                        PeriodId = c.Int(nullable: false, identity: true),
                        PeriodName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PeriodId);
            
            CreateTable(
                "dbo.Promoes",
                c => new
                    {
                        PromoId = c.Int(nullable: false, identity: true),
                        promoName = c.String(),
                        PromoDefId = c.Int(nullable: false),
                        ProductId = c.String(maxLength: 10),
                        PromoGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PromoId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PromoDetails",
                c => new
                    {
                        promoDetailId = c.Int(nullable: false, identity: true),
                        PromoDefId = c.Int(nullable: false),
                        ProductId = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.promoDetailId)
                .ForeignKey("dbo.PromoDefs", t => t.PromoDefId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.PromoDefId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PromoDefs",
                c => new
                    {
                        PromoDefId = c.Int(nullable: false),
                        PromoPrice = c.Double(nullable: false),
                        PromoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PromoDefId)
                .ForeignKey("dbo.Promoes", t => t.PromoDefId)
                .Index(t => t.PromoDefId);
            
            CreateTable(
                "dbo.PromoPeriods",
                c => new
                    {
                        Promo_PromoId = c.Int(nullable: false),
                        Period_PeriodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Promo_PromoId, t.Period_PeriodId })
                .ForeignKey("dbo.Promoes", t => t.Promo_PromoId, cascadeDelete: true)
                .ForeignKey("dbo.Periods", t => t.Period_PeriodId, cascadeDelete: true)
                .Index(t => t.Promo_PromoId)
                .Index(t => t.Period_PeriodId);
            
            AddColumn("dbo.Products", "basePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Discontnuied", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PromoDefs", "PromoDefId", "dbo.Promoes");
            DropForeignKey("dbo.Promoes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PromoDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PromoDetails", "PromoDefId", "dbo.PromoDefs");
            DropForeignKey("dbo.PromoPeriods", "Period_PeriodId", "dbo.Periods");
            DropForeignKey("dbo.PromoPeriods", "Promo_PromoId", "dbo.Promoes");
            DropIndex("dbo.PromoPeriods", new[] { "Period_PeriodId" });
            DropIndex("dbo.PromoPeriods", new[] { "Promo_PromoId" });
            DropIndex("dbo.PromoDefs", new[] { "PromoDefId" });
            DropIndex("dbo.PromoDetails", new[] { "ProductId" });
            DropIndex("dbo.PromoDetails", new[] { "PromoDefId" });
            DropIndex("dbo.Promoes", new[] { "ProductId" });
            DropColumn("dbo.Products", "Discontnuied");
            DropColumn("dbo.Products", "Active");
            DropColumn("dbo.Products", "basePrice");
            DropTable("dbo.PromoPeriods");
            DropTable("dbo.PromoDefs");
            DropTable("dbo.PromoDetails");
            DropTable("dbo.Promoes");
            DropTable("dbo.Periods");
        }
    }
}
