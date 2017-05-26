using DrinkPlaner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    public class ParticipationsViewModel : ChildViewModelBase<PartyViewModel>
    {
        private Party party;
        public ObservableCollection<Participation> Participations { get; private set; }
        public ObservableCollection<Person> NonGuests { get; private set; }
        private bool isLoadingNonGuests;

        public bool IsLoadingNonGuests
        {
            get { return isLoadingNonGuests; }
            set
            {
                if (value != isLoadingNonGuests)
                {
                    isLoadingNonGuests = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public ParticipationsViewModel(ICollection<Participation> participations, Party party, PartyViewModel parentVM) : base(parentVM)
        {
            this.Participations = new ObservableCollection<Participation>(participations);
            this.party = party;
            this.NonGuests = new ObservableCollection<Person>();
            this.LoadPersonsAsync();
        }

        internal void UpdateParent()
        {
            throw new NotImplementedException();
        }

        internal ParticipationViewModel GetViewModelFor(Participation participation)
        {
            return new ParticipationViewModel(participation, this);
        }


        public async override Task SaveModelChangesAsync()
        {
            await this.client.SaveParticipationsAsync(this.Participations.ToList(), party);
        }

        internal void SetGuests(ObservableCollection<Participation> participations)
        {
            this.Participations.Clear();
            foreach (var participation in participations)
            {
                this.Participations.Add(participation);
            }
            this.ModelChanged = true;
        }

        internal void AddGuest(Person person)
        {
            var partcipation = this.party.Guests.SingleOrDefault(p => p.Person.Id == person.Id);
            if (partcipation == null)
            {
                partcipation = this.party.CreateParticipation(person);
            }
            this.Participations.Add(partcipation);
            this.NonGuests.Remove(person);
            this.ModelChanged = true;
            this.ParentVM.ModelHasChanged();
        }

        internal void RemoveGuest(Participation participation)
        {
            this.Participations.Remove(participation);
            this.NonGuests.Add(participation.Person);
            this.ModelChanged = true;
            this.ParentVM.ModelHasChanged();
        }

        private async void LoadPersonsAsync()
        {
            this.NonGuests.Clear();

            var people = await this.client.LoadPeopleAsync();
            foreach (var person in people)
            {
                if (!this.Participations.Any(p => p.Person.Id == person.Id))
                {
                    this.NonGuests.Add(person);
                }
            }
        }

        internal void Refresh()
        {
            var participations = this.Participations.ToList();
            Refresh(participations);
        }

        private void Refresh(IEnumerable<Participation> participations)
        {
            this.Participations.Clear();
            foreach (var participationVM in participations)
            {
                this.Participations.Add(participationVM);
            }
        }
    }
}