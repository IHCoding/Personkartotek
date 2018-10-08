namespace RDB.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AlternativeAddresses", newName: "AlternativeAddress");
            RenameTable(name: "dbo.Providers", newName: "Provider");
            RenameTable(name: "dbo.Countries", newName: "Country");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Country", newName: "Countries");
            RenameTable(name: "dbo.Provider", newName: "Providers");
            RenameTable(name: "dbo.AlternativeAddress", newName: "AlternativeAddresses");
        }
    }
}
