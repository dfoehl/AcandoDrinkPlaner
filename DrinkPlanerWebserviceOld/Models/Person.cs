using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkPlanerWebservice.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teilnahme> Parties { get; set; }
    }
}