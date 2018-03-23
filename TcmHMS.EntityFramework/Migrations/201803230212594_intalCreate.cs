namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TcmDepartments", "DisplayName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.TcmDepartments", "Code", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.TcmDiseases", "DisplayName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.TcmDiseases", "Pinyin", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TcmDiseases", "Pinyin", c => c.String());
            AlterColumn("dbo.TcmDiseases", "DisplayName", c => c.String());
            AlterColumn("dbo.TcmDepartments", "Code", c => c.String());
            AlterColumn("dbo.TcmDepartments", "DisplayName", c => c.String());
        }
    }
}
