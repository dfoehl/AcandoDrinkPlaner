using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkPlanerWebservice.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teilnahme> Gaeste { get; set; }
        public DateTime Zeitpunkt { get; set; }
        public List<Mitbringsel> GetraenkeBedarf { get; set; }

        public Party()
        {
            this.GetraenkeBedarf = new List<Mitbringsel>();
        }
    }
}