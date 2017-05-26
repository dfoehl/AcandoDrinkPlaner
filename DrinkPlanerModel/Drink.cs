using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlaner.Model
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PackageUnitId { get; set; }
        public virtual PackageUnit PackageUnit { get; set; }
        public string Text { get { return this.Name + " - " + this.PackageUnit.Text; } }
    }
}
