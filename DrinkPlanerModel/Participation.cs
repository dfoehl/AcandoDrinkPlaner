using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlaner.Model
{
    public class Participation
    {
        public int Id { get; set; }
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public List<Supply> Supply { get; set; }

        public Participation()
        {
            this.Supply = new List<Model.Supply>();
        }
    }
}
