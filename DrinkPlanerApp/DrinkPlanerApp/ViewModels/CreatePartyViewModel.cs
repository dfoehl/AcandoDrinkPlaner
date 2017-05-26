using DrinkPlaner.Model;
using System;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    class CreatePartyViewModel : ViewModelBase
    {
        private Party party;

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
                    this.RaisePropertyChanged("Time");
                }
            }
        }
        public TimeSpan Time
        {
            get
            {
                return this.Date.TimeOfDay;
            }
            set
            {
                this.Date = this.Date.Date + value;
            }
        }
        public Person Creator
        {
            get { return this.party.Creator; }
        }
        public DateTime Now { get { return DateTime.Now; } }

        public CreatePartyViewModel(WebserviceClient client) : base(client)
        {
            this.party = new Party();
            this.party.Creator = client.SelfPerson;
        }

        internal async Task<PartyViewModel> CreateParty()
        {
            await this.SaveModelChangesAsync();
            return new PartyViewModel(this.client, this.party);
        }

        public override async Task SaveModelChangesAsync()
        {
            this.party = await this.client.SavePartyAsync(this.party);
            this.ModelChanged = false;
        }
    }
}