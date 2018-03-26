namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TcmDisease", "Symptom", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TcmDisease", "Symptom", c => c.String(nullable: false));
        }
    }
}
