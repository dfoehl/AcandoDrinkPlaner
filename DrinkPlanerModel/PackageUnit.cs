using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlaner.Model
{
    public class PackageUnit
    {
        public int Id { get; set; }
        public string UnitSingular { get; set; }
        public string UnitPlural { get; set; }
        public double Value { get; set; }
        public string Text
        {
            get
            {
                return this.Value + " " + (this.Value == 1 ? this.UnitSingular : this.UnitPlural);
            }
        }
    }
}
