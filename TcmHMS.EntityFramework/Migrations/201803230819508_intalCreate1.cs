namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TcmDepartments", newName: "TcmDepartment");
            RenameTable(name: "dbo.TcmDiseases", newName: "TcmDisease");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TcmDisease", newName: "TcmDiseases");
            RenameTable(name: "dbo.TcmDepartment", newName: "TcmDepartments");
        }
    }
}
