using System;

namespace DrinkPlanerWebservice.Models
{
    public class Getraenk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Verpackungseinheit Verpackungseinheit { get; set; }
    }
}