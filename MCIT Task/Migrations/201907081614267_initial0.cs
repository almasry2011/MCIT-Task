namespace MCIT_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        TopicDate = c.DateTime(nullable: false),
                        TopicTitle = c.String(),
                        TopicIsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TopicId);
            
            CreateTable(
                "dbo.TopicDetails",
                c => new
                    {
                        TopicDetailsId = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopicDetailsId)
                .ForeignKey("dbo.topics", t => t.TopicDetailsId)
                .Index(t => t.TopicDetailsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicDetails", "TopicDetailsId", "dbo.topics");
            DropIndex("dbo.TopicDetails", new[] { "TopicDetailsId" });
            DropTable("dbo.TopicDetails");
            DropTable("dbo.topics");
        }
    }
}
