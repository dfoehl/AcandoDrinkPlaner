using DrinkPlaner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private bool isLoading;

        public ObservableCollection<Party> Parties { get; private set; }
        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                if (this.isLoading != value)
                {
                    this.isLoading = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public MainPageViewModel(WebserviceClient client) : base(client)
        {
            this.Parties = new ObservableCollection<Party>();
            this.IsLoading = true;
            this.client.CacheSelfPerson();
            this.IsLoading = false;
        }

        public async Task LoadPartiesAsync()
        {
            this.IsLoading = true;
            this.Parties.Clear();
            var parties = await this.client.LoadPartiesAsync();
            foreach (var party in parties)
            {
                this.Parties.Add(party);
            }
            await Task.Delay(500);
            this.IsLoading = false;
        }

        public override Task SaveModelChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
