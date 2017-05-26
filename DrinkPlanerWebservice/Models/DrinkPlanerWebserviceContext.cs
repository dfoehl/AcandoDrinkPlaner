using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DrinkPlanerWebservice.Models
{
    public class DrinkPlanerWebserviceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DrinkPlanerWebserviceContext() : base("name=DrinkPlanerWebserviceContext")
        {
        }

        public System.Data.Entity.DbSet<DrinkPlaner.Model.PackageUnit> PackageUnits { get; set; }

        public System.Data.Entity.DbSet<DrinkPlaner.Model.Drink> Drinks { get; set; }

        public System.Data.Entity.DbSet<DrinkPlaner.Model.Party> Parties { get; set; }

        public System.Data.Entity.DbSet<DrinkPlaner.Model.Supply> Supplies { get; set; }

        public System.Data.Entity.DbSet<DrinkPlaner.Model.Person> People { get; set; }

        public System.Data.Entity.DbSet<DrinkPlaner.Model.Participation> Participations { get; set; }
    }
}
