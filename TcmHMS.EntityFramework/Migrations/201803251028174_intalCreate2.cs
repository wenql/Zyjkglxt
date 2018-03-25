namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TcmDisease", "Symptom", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TcmDisease", "Symptom");
        }
    }
}
