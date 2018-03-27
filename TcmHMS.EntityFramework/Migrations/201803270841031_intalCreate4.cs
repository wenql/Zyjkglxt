namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifier = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 32),
                        Pinyin = c.String(nullable: false, maxLength: 32),
                        PackagingSpec = c.String(),
                        Function = c.String(),
                        Dosage = c.String(),
                        Source = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 32),
                        Pinyin = c.String(nullable: false, maxLength: 32),
                        DisplayOrder = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ranks");
            DropTable("dbo.Medicines");
        }
    }
}
