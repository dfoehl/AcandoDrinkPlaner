using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkPlanerWebservice.Models
{
    public class Mitbringsel
    {
        public int Id { get; set; }
        public Getraenk Getraenk { get; set; }
        public double Menge { get; set; }
    }
}