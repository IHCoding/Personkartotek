namespace RDB.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlternativeAddresses",
                c => new
                    {
                        AAType = c.String(nullable: false, maxLength: 128),
                        AAID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AAType)
                .ForeignKey("dbo.Address", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        StreetName = c.String(),
                        HouseNumber = c.String(),
                        PostNrID = c.Int(nullable: false),
                        AlternativeAddressID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.PostNr", t => t.PostNrID, cascadeDelete: true)
                .Index(t => t.PostNrID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "dbo.Telefon",
                c => new
                    {
                        TelefonType = c.String(nullable: false, maxLength: 128),
                        TelefonID = c.Int(nullable: false),
                        Number = c.String(),
                        PersonID = c.Int(nullable: false),
                        ProviderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TelefonType)
                .ForeignKey("dbo.Providers", t => t.ProviderID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.ProviderID);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderID = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(),
                    })
                .PrimaryKey(t => t.ProviderID);
            
            CreateTable(
                "dbo.PostNr",
                c => new
                    {
                        PostNrID = c.Int(nullable: false, identity: true),
                        PostNumber = c.String(),
                        CountryID = c.Int(nullable: false),
                        _City_CityID = c.Int(),
                    })
                .PrimaryKey(t => t.PostNrID)
                .ForeignKey("dbo.City", t => t._City_CityID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID)
                .Index(t => t._City_CityID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        PostNumber = c.String(),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.PersonAddresses",
                c => new
                    {
                        Person_PersonID = c.Int(nullable: false),
                        Address_AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_PersonID, t.Address_AddressID })
                .ForeignKey("dbo.Person", t => t.Person_PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Address", t => t.Address_AddressID, cascadeDelete: true)
                .Index(t => t.Person_PersonID)
                .Index(t => t.Address_AddressID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "PostNrID", "dbo.PostNr");
            DropForeignKey("dbo.PostNr", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.PostNr", "_City_CityID", "dbo.City");
            DropForeignKey("dbo.City", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Telefon", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Telefon", "ProviderID", "dbo.Providers");
            DropForeignKey("dbo.PersonAddresses", "Address_AddressID", "dbo.Address");
            DropForeignKey("dbo.PersonAddresses", "Person_PersonID", "dbo.Person");
            DropForeignKey("dbo.AlternativeAddresses", "PersonID", "dbo.Person");
            DropForeignKey("dbo.AlternativeAddresses", "AddressID", "dbo.Address");
            DropIndex("dbo.PersonAddresses", new[] { "Address_AddressID" });
            DropIndex("dbo.PersonAddresses", new[] { "Person_PersonID" });
            DropIndex("dbo.City", new[] { "CountryID" });
            DropIndex("dbo.PostNr", new[] { "_City_CityID" });
            DropIndex("dbo.PostNr", new[] { "CountryID" });
            DropIndex("dbo.Telefon", new[] { "ProviderID" });
            DropIndex("dbo.Telefon", new[] { "PersonID" });
            DropIndex("dbo.Address", new[] { "PostNrID" });
            DropIndex("dbo.AlternativeAddresses", new[] { "AddressID" });
            DropIndex("dbo.AlternativeAddresses", new[] { "PersonID" });
            DropTable("dbo.PersonAddresses");
            DropTable("dbo.Countries");
            DropTable("dbo.City");
            DropTable("dbo.PostNr");
            DropTable("dbo.Providers");
            DropTable("dbo.Telefon");
            DropTable("dbo.Person");
            DropTable("dbo.Address");
            DropTable("dbo.AlternativeAddresses");
        }
    }
}
