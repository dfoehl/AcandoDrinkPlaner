using DrinkPlanerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkPlanerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePartyPage : ContentPage
    {
        private CreatePartyViewModel createPartyVM;
        private INavigation navigation;

        public CreatePartyPage(WebserviceClient client, INavigation navigation)
        {
            this.createPartyVM = new CreatePartyViewModel(client);
            this.BindingContext = this.createPartyVM;
            InitializeComponent();
            this.navigation = navigation;
        }

        private async void CreateParty(object sender, EventArgs e)
        {
            await this.navigation.PushAsync(new PartyDetailPage(await this.createPartyVM.CreateParty()));
            await this.Navigation.PopModalAsync();
        }
    }
}
