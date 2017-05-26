using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlaner.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Participation> Parties { get; set; }

        public Person()
        {
            this.Parties = new List<Participation>();
        }
    }
}
