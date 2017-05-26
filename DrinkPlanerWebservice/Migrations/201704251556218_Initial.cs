namespace DrinkPlanerWebservice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PackageUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackageUnits", t => t.PackageUnitId, cascadeDelete: true)
                .Index(t => t.PackageUnitId);
            
            CreateTable(
                "dbo.PackageUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitSingular = c.String(),
                        UnitPlural = c.String(),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Parties", t => t.PartyId, cascadeDelete: true)
                .Index(t => t.PartyId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Parties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatorId = c.Int(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrinkId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Party_Id = c.Int(),
                        Participation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drinks", t => t.DrinkId, cascadeDelete: true)
                .ForeignKey("dbo.Parties", t => t.Party_Id)
                .ForeignKey("dbo.Participations", t => t.Participation_Id)
                .Index(t => t.DrinkId)
                .Index(t => t.Party_Id)
                .Index(t => t.Participation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplies", "Participation_Id", "dbo.Participations");
            DropForeignKey("dbo.Supplies", "Party_Id", "dbo.Parties");
            DropForeignKey("dbo.Supplies", "DrinkId", "dbo.Drinks");
            DropForeignKey("dbo.Participations", "PartyId", "dbo.Parties");
            DropForeignKey("dbo.Parties", "CreatorId", "dbo.People");
            DropForeignKey("dbo.Participations", "PersonId", "dbo.People");
            DropForeignKey("dbo.Drinks", "PackageUnitId", "dbo.PackageUnits");
            DropIndex("dbo.Supplies", new[] { "Participation_Id" });
            DropIndex("dbo.Supplies", new[] { "Party_Id" });
            DropIndex("dbo.Supplies", new[] { "DrinkId" });
            DropIndex("dbo.Parties", new[] { "CreatorId" });
            DropIndex("dbo.Participations", new[] { "PersonId" });
            DropIndex("dbo.Participations", new[] { "PartyId" });
            DropIndex("dbo.Drinks", new[] { "PackageUnitId" });
            DropTable("dbo.Supplies");
            DropTable("dbo.People");
            DropTable("dbo.Parties");
            DropTable("dbo.Participations");
            DropTable("dbo.PackageUnits");
            DropTable("dbo.Drinks");
        }
    }
}
