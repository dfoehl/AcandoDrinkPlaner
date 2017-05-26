using DrinkPlanerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrinkPlanerApp.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel partiesVM;
        public MainPage()
        {
            partiesVM = new MainPageViewModel(new WebserviceClient());
            this.BindingContext = partiesVM;
            InitializeComponent();
        }

        private async void PartyTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new PartyDetailPage(new PartyViewModel(partiesVM.client, (DrinkPlaner.Model.Party)e.Item)));
        }

        private async void PartiesRefreshing(object sender, EventArgs e)
        {
            await partiesVM.LoadPartiesAsync();
            ((ListView)sender).EndRefresh();
        }

        private void AddParty(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new CreatePartyPage(partiesVM.client, this.Navigation));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await this.partiesVM.LoadPartiesAsync();
        }
    }
}
