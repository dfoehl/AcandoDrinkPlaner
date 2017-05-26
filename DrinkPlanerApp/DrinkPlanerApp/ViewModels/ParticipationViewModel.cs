using System;
using System.Linq;
using System.Threading.Tasks;
using DrinkPlaner.Model;
using DrinkPlanerApp.ViewModels;
using System.Collections.ObjectModel;

namespace DrinkPlanerApp.ViewModels
{
    public class ParticipationViewModel : ChildViewModelBase<ParticipationsViewModel>
    {
        private Participation participation;

        public Person Person { get { return this.participation.Person; } }
        public ObservableCollection<SupplyViewModel> Supplies { get; private set; }
        public bool IsSelfParticipation { get { return this.participation.PersonId == this.client.SelfPeopleId; } }

        public ParticipationViewModel(Participation participation, ParticipationsViewModel parentVM) : base(parentVM)
        {
            this.participation = participation;
            this.Supplies = new ObservableCollection<ViewModels.SupplyViewModel>(
                participation.Supply.Select(
                    s => new SupplyViewModel(
                        s,
                        this.client,
                        () => this.ModelHasChanged(),
                        (sup) =>
                        {
                            this.Supplies.Remove(sup);
                            this.ModelHasChanged();
                        }
                    )
                )
            );
        }

        public SupplyViewModel CreateNewSupplyViewModel()
        {
            var vm = new SupplyViewModel(new Supply(),
                this.client,
                () => this.ModelHasChanged(),
                (sup) =>
                {
                    this.Supplies.Remove(sup);
                    this.ModelHasChanged();
                });
            this.Supplies.Add(vm);
            return vm;
        }

        public async override Task SaveModelChangesAsync()
        {
            await this.client.SaveSuppliesAsync(this.Supplies.Select(vm => vm.Supply).ToList(), this.participation);
            this.participation.Supply.Clear();
            this.participation.Supply.AddRange(this.Supplies.Select(s => s.Supply));
            this.ModelChanged = false;
        }
    }
}