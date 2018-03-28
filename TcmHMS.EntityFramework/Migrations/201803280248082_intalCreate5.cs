namespace TcmHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intalCreate5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Medicines", newName: "TcmMedicine");
            RenameTable(name: "dbo.Ranks", newName: "TcmRank");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TcmRank", newName: "Ranks");
            RenameTable(name: "dbo.TcmMedicine", newName: "Medicines");
        }
    }
}
