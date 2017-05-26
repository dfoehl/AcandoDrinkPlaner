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
    public partial class PartyManageGuestsPage : ContentPage
    {
        private ParticipationsViewModel participationsVM;
        
        public PartyManageGuestsPage(ParticipationsViewModel participationsViewModel)
        {
            this.participationsVM = participationsViewModel;
            this.BindingContext = participationsViewModel;
            InitializeComponent();
        }

        private void HandleAddGuest(object sender, ItemTappedEventArgs e)
        {
            this.participationsVM.AddGuest((DrinkPlaner.Model.Person)e.Item);
        }

        private void HandleRemoveGuest(object sender, ItemTappedEventArgs e)
        {
            this.participationsVM.RemoveGuest((DrinkPlaner.Model.Participation)e.Item);
        }
    }
}
