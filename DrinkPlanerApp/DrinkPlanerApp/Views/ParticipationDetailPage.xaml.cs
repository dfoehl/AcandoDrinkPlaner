using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkPlanerApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkPlanerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParticipationDetailPage : ContentPage
    {
        private ParticipationViewModel participationVM;

        public ParticipationDetailPage(ParticipationViewModel participationViewModel)
        {
            this.participationVM = participationViewModel;
            this.BindingContext = this.participationVM;
            InitializeComponent();
        }

        private async void SupplySelected(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new SupplyDetailPage((SupplyViewModel)e.Item));
        }

        private async void NewSupply(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new SupplyDetailPage(this.participationVM.CreateNewSupplyViewModel()));
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            await this.participationVM.SaveModelChangesAsync();
        }
    }
}
