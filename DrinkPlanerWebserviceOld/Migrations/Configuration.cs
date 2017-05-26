namespace DrinkPlanerWebservice.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DrinkPlanerWebservice.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DrinkPlanerWebservice.Models.DrinkPlanerWebserviceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DrinkPlanerWebservice.Models.DrinkPlanerWebserviceContext";
        }

        protected override void Seed(DrinkPlanerWebservice.Models.DrinkPlanerWebserviceContext context)
        {
            Verpackungseinheit[] verpackungseinheiten = new Verpackungseinheit[]
            {
                new Verpackungseinheit { Einheit="Liter", Wert = 1.5 },
                new Verpackungseinheit { Einheit="Packung", Wert = 1 },
                new Verpackungseinheit { Einheit="Kiste", Wert = 0.5 }
            };

            context.Verpackungseinheits.AddOrUpdate(verpackungseinheiten);

            Getraenk[] getraenke = new Getraenk[]
            {
                new Getraenk { Name = "Coca Cola", Verpackungseinheit = verpackungseinheiten[0] },
                new Getraenk { Name = "Orangensaft", Verpackungseinheit = verpackungseinheiten[1] },
                new Getraenk { Name = "Bier", Verpackungseinheit = verpackungseinheiten[2] }
            };

            context.Getraenks.AddOrUpdate(getraenke);

            Party[] parties = new Party[]
            {
                new Party { Name = "MeetIT Aftershow", Zeitpunkt= new DateTime(2017, 05, 16, 17, 30, 00) },
                new Party { Name = "B-Meeting Frankfurt", Zeitpunkt = new DateTime(2017, 06, 01, 19,00,00) }
            };

            context.Parties.AddOrUpdate(parties);

            Mitbringsel[] mitbringsel = new Mitbringsel[]
            {
                new Mitbringsel { Getraenk = getraenke[0], Menge = 10 },
                new Mitbringsel { Getraenk = getraenke[2], Menge = 20 },
                new Mitbringsel { Getraenk = getraenke[2], Menge = 40 }
            };

            context.Mitbringsels.AddOrUpdate(mitbringsel);

            parties[0].GetraenkeBedarf.Add(mitbringsel[0]);
            parties[0].GetraenkeBedarf.Add(mitbringsel[1]);
            parties[1].GetraenkeBedarf.Add(mitbringsel[2]);

            context.Parties.AddOrUpdate(parties);
        }
    }
}
