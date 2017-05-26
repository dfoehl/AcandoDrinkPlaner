using DrinkPlaner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    public class SupplyViewModel : ViewModelBase
    {
        private Supply supply;
        private Action<SupplyViewModel> deleteHandler;
        private Action updateHandler;

        public Supply Supply { get { return this.supply; } }
        public Drink Drink
        {
            get { return this.supply.Drink; }
            set
            {
                if (value != this.supply.Drink)
                {
                    this.supply.Drink = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged("Unit");
                    this.ModelHasChanged();
                }
            }
        }

        public ObservableCollection<Drink> Drinks { get; private set; }
        public double Amount
        {
            get { return this.supply.Amount; }
            set
            {
                if (value != this.supply.Amount)
                {
                    this.supply.Amount = value;
                    this.RaisePropertyChanged();
                    this.ModelHasChanged();
                }
            }
        }
        public string Unit { get { return this.supply.Drink?.PackageUnit.UnitPlural; } }

        public SupplyViewModel(Supply supply, WebserviceClient client, Action updateHandler, Action<SupplyViewModel> deleteHandler) : base(client)
        {
            this.supply = supply;
            this.Drinks = new ObservableCollection<Drink>();
            this.updateHandler = updateHandler;
            this.deleteHandler = deleteHandler;

            this.LoadDrinksAsync();
        }

        private async void LoadDrinksAsync()
        {
            var drinks = await this.client.LoadDrinksAsync();
            this.Drinks.Clear();
            foreach (var drink in drinks)
            {
                if (this.Drink != null && this.Drink.Id == drink.Id)
                    this.Drinks.Add(this.Drink);
                else
                    this.Drinks.Add(drink);
            }
        }

        public async override Task SaveModelChangesAsync()
        {
            await Task.Run(() =>
            {
                if (this.ModelChanged)
                    this.updateHandler();
            });
        }

        internal void DeleteSupply()
        {
            this.deleteHandler(this);
        }
    }
}
