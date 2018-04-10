namespace TcmHMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TcmDoctor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        HeadImg = c.String(),
                        Hospital = c.String(),
                        RankId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        NickName = c.String(maxLength: 128),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Address = c.String(),
                        GoodAt = c.String(),
                        Introduction = c.String(),
                        IsActice = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Doctor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TcmDepartment", t => t.DepartmentId)
                .ForeignKey("dbo.TcmRank", t => t.RankId)
                .Index(t => t.RankId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TcmDoctor", "RankId", "dbo.TcmRank");
            DropForeignKey("dbo.TcmDoctor", "DepartmentId", "dbo.TcmDepartment");
            DropIndex("dbo.TcmDoctor", new[] { "DepartmentId" });
            DropIndex("dbo.TcmDoctor", new[] { "RankId" });
            DropTable("dbo.TcmDoctor",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Doctor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
