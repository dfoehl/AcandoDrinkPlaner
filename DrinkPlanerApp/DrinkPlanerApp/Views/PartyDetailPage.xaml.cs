using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkPlanerApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DrinkPlanerApp.Views;
using System.Diagnostics;

namespace DrinkPlanerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyDetailPage : ContentPage
    {
        private PartyViewModel partyVM;

        public PartyDetailPage(PartyViewModel partyViewModel)
        {
            this.partyVM = partyViewModel;
            this.BindingContext = this.partyVM;
            InitializeComponent();
        }

        private async void AddGuest(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new PartyManageGuestsPage(this.partyVM.ParticipationsVM));
        }

        private async void Save(object sender, EventArgs e)
        {
            await this.partyVM.SaveModelChangesAsync();
        }

        private async void AddSupply(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new SupplyDetailPage(this.partyVM.SuppliesVM.CreateNewViewModel()));
        }

        private async void SupplySelected(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new SupplyDetailPage((SupplyViewModel)e.Item));
        }

        private async void ParticipationSelected(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new ParticipationDetailPage(this.partyVM.ParticipationsVM.GetViewModelFor((DrinkPlaner.Model.Participation)e.Item)));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await this.partyVM.RefreshSuppliesAsync();
            await this.partyVM.RefreshParticipationsAsync();
        }
    }
}
