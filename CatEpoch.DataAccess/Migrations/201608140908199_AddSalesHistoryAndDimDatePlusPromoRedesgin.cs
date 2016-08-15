namespace CatEpoch.DataAccess.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddSalesHistoryAndDimDatePlusPromoRedesgin : DbMigration
	{
		public override void Up()
		{
			MoveTable(name: "dbo.Promos", newSchema: "Promo");
			MoveTable(name: "dbo.Products", newSchema: "Product");
			MoveTable(name: "dbo.ProductGroups", newSchema: "Product");
			MoveTable(name: "dbo.ProductTrees", newSchema: "Product");
			DropForeignKey("dbo.PromoDetails", "PromoDefId", "dbo.PromoDefs");
			DropForeignKey("dbo.PromoDetails", "Product_Id", "dbo.Products");
			DropForeignKey("dbo.PromoDefs", "PromoDefId", "dbo.Promos");
			DropIndex("dbo.PromoDetails", new[] { "PromoDefId" });
			DropIndex("dbo.PromoDetails", new[] { "Product_Id" });
			DropIndex("dbo.PromoDefs", new[] { "PromoDefId" });
			CreateTable(
				"Warehouse.DimDates",
				c => new
					{
						DateKey = c.Int(nullable: false),
						PeriodId = c.Int(nullable: false),
						DateAltKey = c.DateTime(nullable: false),
						Year = c.Int(nullable: false),
						Quarter = c.Int(nullable: false),
						Month = c.Int(nullable: false),
						MonthName = c.String(),
						DayOfMonth = c.Int(nullable: false),
						DayOfWeek = c.Int(nullable: false),
						DayName = c.String(),
						WeekOfMonth = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.DateKey)
				.ForeignKey("dbo.Periods", t => t.PeriodId, cascadeDelete: true)
				.Index(t => t.PeriodId);
			
			CreateTable(
				"WareHouse.SalesHistories",
				c => new
					{
						ProductId = c.String(nullable: false, maxLength: 10),
						Month = c.String(nullable: false, maxLength: 128),
						Year = c.Int(nullable: false),
						GroupId = c.Int(nullable: false),
						PromoId = c.Int(nullable: false),
						Units = c.Int(nullable: false),
						Value = c.Int(nullable: false),
						Forecast = c.Int(nullable: false),
						CountOutDays = c.Int(nullable: false),
						Booking = c.Boolean(nullable: false),
						Price = c.Single(nullable: false),
					})
				.PrimaryKey(t => new { t.ProductId, t.Month, t.Year })
				.ForeignKey("Product.ProductGroups", t => t.GroupId, cascadeDelete: true)
				.ForeignKey("Product.Products", t => t.ProductId)
				.ForeignKey("Promo.Promos", t => t.PromoId)
				.Index(t => t.ProductId)
				.Index(t => t.GroupId)
				.Index(t => t.PromoId);
			
			CreateTable(
				"dbo.PromoProducts",
				c => new
					{
						PromoProductId = c.Int(nullable: false, identity: true),
						ProductId = c.String(maxLength: 10),
						PromoId = c.Int(nullable: false),
						PromoPrice = c.Single(nullable: false),
						Qty = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.PromoProductId)
				.ForeignKey("Promo.Promos", t => t.PromoId, cascadeDelete: true)
				.ForeignKey("Product.Products", t => t.ProductId, cascadeDelete: true)
				.Index(t => t.ProductId)
				.Index(t => t.PromoId);
			
			AlterColumn("Product.Products", "OldId", c => c.String(maxLength: 10));
			AlterColumn("Product.Products", "ProductName", c => c.String(nullable: false, maxLength: 55));
			AlterColumn("Product.Products", "BasePrice", c => c.Single(nullable: false));
			AlterColumn("Product.ProductGroups", "GroupName", c => c.String(maxLength: 55));
			DropColumn("Promo.Promos", "PromoPrice");
			DropColumn("Promo.Promos", "PromoDefId");
			DropTable("dbo.PromoDetails");
			DropTable("dbo.PromoDefs");
			
            CreateStoredProcedure(
				"dbo.FillDimDate",
				p => new
					{
						STARTDATE = p.DateTime(),
						ENDDATE = p.DateTime(),
						LOOPDATE = p.DateTime()
					},
				body:
					@" SET @ENDDATE=getdate()
					   SET @LOOPDATE = @STARTDATE
					   WHILE @LOOPDATE <= @ENDDATE
					   BEGIN
 
					   INSERT [dbo].[DimDates]([DateKey], [DateAltKey], [Year], [Quarter], [Month], [MonthName], [DayOfMonth], [DayOfWeek], [DayName], [WeekOfMonth])
						  VALUES (
						  CAST(CONVERT(VARCHAR(8),@LOOPDATE,112)AS INT),
						  @LOOPDATE,
						  YEAR(@LOOPDATE),
						  Datepart(qq,@LOOPDATE),
						  Month(@LOOPDATE),
						  datename(mm,@LOOPDATE),
						  Day(@LOOPDATE),
						  datepart(dw,@LOOPDATE),
						  datename(dw,@LOOPDATE),
						  DATEPART(WEEK, @LOOPDATE)  - DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,@LOOPDATE), 0))+ 1 
						  )
						  SET @LOOPDATE = DateAdd(dd,1,@LOOPDATE)
					  END"
			);
			
			
			
		}
		
		public override void Down()
		{
			DropStoredProcedure("dbo.FillDimDate");
			CreateTable(
				"dbo.PromoDefs",
				c => new
					{
						PromoDefId = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.PromoDefId);
			
			CreateTable(
				"dbo.PromoDetails",
				c => new
					{
						promoDetailId = c.Int(nullable: false, identity: true),
						PromoDefId = c.Int(nullable: false),
						Product_Id = c.String(maxLength: 10),
					})
				.PrimaryKey(t => t.promoDetailId);
			
			AddColumn("Promo.Promos", "PromoDefId", c => c.Int(nullable: false));
			AddColumn("Promo.Promos", "PromoPrice", c => c.Double(nullable: false));
			DropForeignKey("Warehouse.DimDates", "PeriodId", "dbo.Periods");
			DropForeignKey("WareHouse.SalesHistories", "PromoId", "Promo.Promos");
			DropForeignKey("WareHouse.SalesHistories", "ProductId", "Product.Products");
			DropForeignKey("dbo.PromoProducts", "ProductId", "Product.Products");
			DropForeignKey("dbo.PromoProducts", "PromoId", "Promo.Promos");
			DropForeignKey("WareHouse.SalesHistories", "GroupId", "Product.ProductGroups");
			DropIndex("dbo.PromoProducts", new[] { "PromoId" });
			DropIndex("dbo.PromoProducts", new[] { "ProductId" });
			DropIndex("WareHouse.SalesHistories", new[] { "PromoId" });
			DropIndex("WareHouse.SalesHistories", new[] { "GroupId" });
			DropIndex("WareHouse.SalesHistories", new[] { "ProductId" });
			DropIndex("Warehouse.DimDates", new[] { "PeriodId" });
			AlterColumn("Product.ProductGroups", "GroupName", c => c.String());
			AlterColumn("Product.Products", "BasePrice", c => c.Double(nullable: false));
			AlterColumn("Product.Products", "ProductName", c => c.String());
			AlterColumn("Product.Products", "OldId", c => c.String());
			DropTable("dbo.PromoProducts");
			DropTable("WareHouse.SalesHistories");
			DropTable("Warehouse.DimDates");
			CreateIndex("dbo.PromoDefs", "PromoDefId");
			CreateIndex("dbo.PromoDetails", "Product_Id");
			CreateIndex("dbo.PromoDetails", "PromoDefId");
			AddForeignKey("dbo.PromoDefs", "PromoDefId", "dbo.Promos", "PromoId", cascadeDelete: true);
			AddForeignKey("dbo.PromoDetails", "Product_Id", "dbo.Products", "Id");
			AddForeignKey("dbo.PromoDetails", "PromoDefId", "dbo.PromoDefs", "PromoDefId", cascadeDelete: true);
			MoveTable(name: "Product.ProductTrees", newSchema: "dbo");
			MoveTable(name: "Product.ProductGroups", newSchema: "dbo");
			MoveTable(name: "Product.Products", newSchema: "dbo");
			MoveTable(name: "Promo.Promos", newSchema: "dbo");
		}
	}
}
