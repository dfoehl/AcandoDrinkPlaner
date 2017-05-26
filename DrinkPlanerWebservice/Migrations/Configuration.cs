namespace DrinkPlanerWebservice.Migrations
{
    using DrinkPlaner.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DrinkPlanerWebservice.Models.DrinkPlanerWebserviceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DrinkPlanerWebservice.Models.DrinkPlanerWebserviceContext context)
        {
            Person[] people = new Person[]
            {
                new Person { Name = "Dominik Föhl" },
                new Person { Name = "Werner Gerstmayr" }
            };
            context.People.AddOrUpdate(people);

            PackageUnit[] packageUnit = new PackageUnit[]
            {
                new PackageUnit { UnitSingular="Liter", UnitPlural="Liter", Value = 1.5 },
                new PackageUnit { UnitSingular="Packung", UnitPlural="Packungen", Value = 1 },
                new PackageUnit { UnitSingular="Kiste", UnitPlural="Kisten", Value = 0.5 }
            };

            context.PackageUnits.AddOrUpdate(packageUnit);

            Drink[] drinks = new Drink[]
            {
                new Drink { Name = "Coca Cola", PackageUnit = packageUnit[0] },
                new Drink { Name = "Orangensaft", PackageUnit = packageUnit[1] },
                new Drink { Name = "Bier", PackageUnit = packageUnit[2] }
            };

            context.Drinks.AddOrUpdate(drinks);

            Party[] parties = new Party[]
            {
                new Party { Name = "MeetIT Aftershow", Date= new DateTime(2017, 05, 16, 17, 30, 00), Creator = people[0] },
                new Party { Name = "B-Meeting Frankfurt", Date = new DateTime(2017, 06, 01, 19,00,00), Creator = people[1] }
            };

            context.Parties.AddOrUpdate(parties);

            Supply[] supplies = new Supply[]
            {
                new Supply { Drink = drinks[0], Amount = 10 },
                new Supply { Drink = drinks[2], Amount = 20 },
                new Supply { Drink = drinks[2], Amount = 40 }
            };

            context.Supplies.AddOrUpdate(supplies);

            parties[0].NeededDrinks.Add(supplies[0]);
            parties[0].NeededDrinks.Add(supplies[1]);
            parties[1].NeededDrinks.Add(supplies[2]);

            parties[0].AddGuest(people[0], supplies[0], supplies[1]);
            parties[0].AddGuest(people[1]);
            parties[1].AddGuest(people[1], supplies[2]);

            context.Parties.AddOrUpdate(parties);
        }
    }
}
