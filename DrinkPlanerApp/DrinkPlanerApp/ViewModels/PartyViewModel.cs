using DrinkPlaner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    public class PartyViewModel : ViewModelBase
    {
        protected Party party;
        protected ParticipationsViewModel participationsVM;
        protected SuppliesViewModel suppliesVM;

        public string Name
        {
            get
            {
                return this.party.Name;
            }
            set
            {
                if (value != this.party.Name)
                {
                    this.party.Name = value;
                    this.ModelChanged = true;
                    this.RaisePropertyChanged();
                }
            }
        }
        public DateTime Date
        {
            get
            {
                return this.party.Date;
            }
            set
            {
                if (value != this.party.Date)
                {
                    this.party.Date = value;
                    this.ModelChanged = true;
                    this.RaisePropertyChanged();
                }
            }
        }
        public Person Creator
        {
            get
            {
                return this.party.Creator;
            }
            set
            {
                if (this.party.Creator != value)
                {
                    this.party.Creator = value;
                    this.ModelChanged = true;
                    this.RaisePropertyChanged();
                }
            }
        }
        public ParticipationsViewModel ParticipationsVM { get { return this.participationsVM; } }
        public SuppliesViewModel SuppliesVM { get { return this.suppliesVM; } }

        public bool CanEditDrinks { get { return this.client.SelfPeopleId == this.Creator.Id; } }

        public PartyViewModel(WebserviceClient client, Party party) : base(client)
        {
            this.party = party;
            this.participationsVM = new ParticipationsViewModel(party.Guests, this.party, this);
            this.suppliesVM = new SuppliesViewModel(party.NeededDrinks, this.party, this);
        }

        public PartyViewModel(PartyViewModel partyVM) : this(partyVM.client, partyVM.party) { }

        public PartyViewModel(WebserviceClient client, Party party, bool modelChanged) : this(client, party)
        {
            this.ModelChanged = modelChanged;
        }

        public override async Task SaveModelChangesAsync()
        {
            await this.participationsVM.SaveModelChangesAsync();
            await this.suppliesVM.SaveModelChangesAsync();
            this.party = await this.client.SavePartyAsync(this.party);
            this.ModelChanged = false;
        }

        internal void SetGuests(ObservableCollection<Participation> guests)
        {
            this.participationsVM.SetGuests(guests);
            this.ModelChanged = true;
        }

        internal async Task RefreshSuppliesAsync()
        {
            this.suppliesVM.Refresh();
        }

        internal async Task RefreshParticipationsAsync()
        {
            this.participationsVM.Refresh();
        }
    }
}
