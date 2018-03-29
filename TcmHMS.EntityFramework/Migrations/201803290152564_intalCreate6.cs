namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TcmConstitutionSubjectOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 50),
                        DisplayOrder = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TcmConstitutionSubject", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.TcmConstitutionSubject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        SpecifyGebder = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TcmConstitutionSuggest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        GeneralFeature = c.String(maxLength: 500),
                        ShapeFeature = c.String(maxLength: 500),
                        CommonPerformance = c.String(maxLength: 500),
                        PsychologyFeature = c.String(maxLength: 500),
                        DiseaseTendency = c.String(maxLength: 500),
                        Adaptability = c.String(maxLength: 500),
                        ExerciseNurse = c.String(maxLength: 500),
                        MedicineNurse = c.String(maxLength: 500),
                        NurseMethod = c.String(maxLength: 500),
                        HealthyRecipes = c.String(maxLength: 500),
                        IsEnabled = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TcmConstitutionSubjectOption", "SubjectId", "dbo.TcmConstitutionSubject");
            DropIndex("dbo.TcmConstitutionSubjectOption", new[] { "SubjectId" });
            DropTable("dbo.TcmConstitutionSuggest");
            DropTable("dbo.TcmConstitutionSubject");
            DropTable("dbo.TcmConstitutionSubjectOption");
        }
    }
}
