using DrinkPlaner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    public class SuppliesViewModel : ChildViewModelBase<PartyViewModel>
    {
        private Party party;
        public ObservableCollection<SupplyViewModel> Supplies { get; private set; }

        public SuppliesViewModel(ICollection<Supply> supplies, Party party, PartyViewModel parentVM) : base(parentVM)
        {
            this.Supplies = new ObservableCollection<SupplyViewModel>();
            this.Refresh(
                supplies.Select(
                    s => new SupplyViewModel(
                        s,
                        this.client,
                        () => this.SuppliesChanged(),
                        (sup) =>
                            {
                                this.Supplies.Remove(sup);
                                this.SuppliesChanged();
                            }
                    )
                )
            );
            this.party = party;
        }

        public async override Task SaveModelChangesAsync()
        {
            await this.client.SaveSuppliesAsync(this.Supplies.Select(vm => vm.Supply).ToList(), this.party);
            this.ModelChanged = false;
        }

        internal SupplyViewModel CreateNewViewModel()
        {
            var supply = new Supply();
            this.Supplies.Add(this.CreateViewModelFor(supply));
            return this.CreateViewModelFor(supply);
        }

        internal SupplyViewModel CreateViewModelFor(Supply supply)
        {
            return new SupplyViewModel(
                supply,
                this.client,
                () => this.SuppliesChanged(),
                (sup) =>
                {
                    this.Supplies.Remove(sup);
                    this.SuppliesChanged();
                });
        }

        internal void SuppliesChanged()
        {
            this.ParentVM.ModelHasChanged();
        }

        internal void Refresh()
        {
            var supplies = this.Supplies.ToList();
            Refresh(supplies);
        }

        private void Refresh(IEnumerable<SupplyViewModel> supplies)
        {
            this.Supplies.Clear();
            foreach (var supplyVM in supplies)
            {
                this.Supplies.Add(supplyVM);
            }
        }
    }
}