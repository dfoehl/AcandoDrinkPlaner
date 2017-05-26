using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlaner.Model
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Participation> Guests { get; set; }
        public int? CreatorId { get; set; }
        public virtual Person Creator { get; set; }
        public DateTime Date { get; set; }
        public List<Supply> NeededDrinks { get; set; }

        public Party()
        {
            this.NeededDrinks = new List<Supply>();
            this.Guests = new List<Participation>();
        }

        public void AddGuest(Person person, params Supply[] supplies)
        {
            Participation participation = CreateParticipation(person, supplies);
            AddParticipation(participation);
        }

        public void AddParticipation(Participation participation)
        {
            this.Guests.Add(participation);
        }

        public Participation CreateParticipation(Person person, params Supply[] supplies)
        {
            var participation = new Participation();
            participation.Party = this;
            participation.PartyId = this.Id;
            participation.Person = person;
            participation.PersonId = person.Id;
            participation.Supply.AddRange(supplies);
            return participation;
        }
    }
}
