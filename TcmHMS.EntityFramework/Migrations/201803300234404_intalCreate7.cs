namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TcmConstitutionSuggest", "IsDefault", c => c.Boolean(nullable: false));
            DropColumn("dbo.TcmConstitutionSuggest", "IsEnabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TcmConstitutionSuggest", "IsEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.TcmConstitutionSuggest", "IsDefault");
        }
    }
}
