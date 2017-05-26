namespace DrinkPlanerWebservice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Getraenks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Verpackungseinheit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Verpackungseinheits", t => t.Verpackungseinheit_Id)
                .Index(t => t.Verpackungseinheit_Id);
            
            CreateTable(
                "dbo.Verpackungseinheits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Einheit = c.String(),
                        Wert = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mitbringsels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Menge = c.Double(nullable: false),
                        Getraenk_Id = c.Int(),
                        Teilnahme_Id = c.Int(),
                        Party_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Getraenks", t => t.Getraenk_Id)
                .ForeignKey("dbo.Teilnahmes", t => t.Teilnahme_Id)
                .ForeignKey("dbo.Parties", t => t.Party_Id)
                .Index(t => t.Getraenk_Id)
                .Index(t => t.Teilnahme_Id)
                .Index(t => t.Party_Id);
            
            CreateTable(
                "dbo.Parties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Zeitpunkt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teilnahmes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Party_Id = c.Int(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parties", t => t.Party_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Party_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mitbringsels", "Party_Id", "dbo.Parties");
            DropForeignKey("dbo.Teilnahmes", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Teilnahmes", "Party_Id", "dbo.Parties");
            DropForeignKey("dbo.Mitbringsels", "Teilnahme_Id", "dbo.Teilnahmes");
            DropForeignKey("dbo.Mitbringsels", "Getraenk_Id", "dbo.Getraenks");
            DropForeignKey("dbo.Getraenks", "Verpackungseinheit_Id", "dbo.Verpackungseinheits");
            DropIndex("dbo.Teilnahmes", new[] { "Person_Id" });
            DropIndex("dbo.Teilnahmes", new[] { "Party_Id" });
            DropIndex("dbo.Mitbringsels", new[] { "Party_Id" });
            DropIndex("dbo.Mitbringsels", new[] { "Teilnahme_Id" });
            DropIndex("dbo.Mitbringsels", new[] { "Getraenk_Id" });
            DropIndex("dbo.Getraenks", new[] { "Verpackungseinheit_Id" });
            DropTable("dbo.People");
            DropTable("dbo.Teilnahmes");
            DropTable("dbo.Parties");
            DropTable("dbo.Mitbringsels");
            DropTable("dbo.Verpackungseinheits");
            DropTable("dbo.Getraenks");
        }
    }
}
