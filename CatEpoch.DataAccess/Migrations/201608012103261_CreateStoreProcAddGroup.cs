namespace CatEpoch.DataAccess.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class CreateStoreProcAddGroup : DbMigration
	{
		public override void Up()
		{
			Sql("ALTER TABLE dbo.ProductTrees ADD OrgLevel AS (OrgNode.GetLevel())");
			Sql("CREATE UNIQUE INDEX ProductGroupOrgNc1 ON dbo.ProductTrees(OrgLevel, OrgNode)");
			CreateStoredProcedure(
				"dbo.AddGroup",
				p => new
				{
					grname = p.String(),
					prntid = p.Int()
				},
				body:
						   @"  DECLARE @pOrgNode hierarchyid, @lc hierarchyid ,@grpid int
   SELECT @pOrgNode = [OrgNode] 
   FROM [dbo].[ProductTrees] 
   WHERE [GroupId] = @prntid
   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
   BEGIN TRANSACTION
	  SELECT @lc = max([OrgNode]) 
	  FROM [dbo].[ProductTrees]
	  WHERE [OrgNode].GetAncestor(1) =@pOrgNode ;
	  INSERT [dbo].[ProductTrees] ([TreeName])
	  VALUES(@grname)
	  SELECT @grpid=Id
	  FROM [dbo].[ProductGroups]
	  WHERE [GroupName]=@grname
	  INSERT [dbo].[ProductTrees] ([OrgNode], [GroupId],[ParentId], [TreeName])
	  VALUES(@pOrgNode.GetDescendant(@lc, NULL), @grpid, @prntid, @grname)
	  COMMIT"
			);
		}
		
		public override void Down()
		{
		}
	}
}
