namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TcmDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Code = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        Description = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TcmDiseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        DisplayName = c.String(),
                        Pinyin = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        Description = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TcmDepartments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TcmDiseases", "DepartmentId", "dbo.TcmDepartments");
            DropIndex("dbo.TcmDiseases", new[] { "DepartmentId" });
            DropTable("dbo.TcmDiseases");
            DropTable("dbo.TcmDepartments");
        }
    }
}
