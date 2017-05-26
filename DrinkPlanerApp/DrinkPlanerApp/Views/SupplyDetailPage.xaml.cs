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
    public partial class SupplyDetailPage : ContentPage
    {
        private SupplyViewModel supplyVM;

        public SupplyDetailPage(SupplyViewModel supplyVM)
        {
            this.supplyVM = supplyVM;
            this.BindingContext = this.supplyVM;
            InitializeComponent();
        }

        private async void DeleteSupplyAsync(object sender, EventArgs e)
        {
            this.supplyVM.DeleteSupply();
            await this.Navigation.PopAsync();
        }

        protected async override void OnDisappearing()
        {
            await this.supplyVM.SaveModelChangesAsync();
            base.OnDisappearing();
        }
    }
}
