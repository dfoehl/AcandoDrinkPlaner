using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlaner.Model
{
    public class Supply
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public virtual Drink Drink { get; set; }
        public double Amount { get; set; }

    }
}
