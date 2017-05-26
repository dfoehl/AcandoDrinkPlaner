using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkPlanerWebservice.Models
{
    public class Teilnahme
    {
        public int Id { get; set; }
        public Party Party { get; set; }
        public Person Person { get; set; }
        public List<Mitbringsel> Mitbringsel { get; set; }
    }
}