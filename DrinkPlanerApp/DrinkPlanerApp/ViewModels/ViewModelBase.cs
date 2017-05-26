using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlanerApp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public readonly WebserviceClient client;
        private bool modelChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool ModelChanged
        {
            get { return this.modelChanged; }
            protected set
            {
                if (value != this.modelChanged)
                {
                    this.modelChanged = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase(WebserviceClient client)
        {
            this.client = client;
        }

        public void ModelHasChanged()
        {
            this.ModelChanged = true;
        }

        public abstract Task SaveModelChangesAsync();
    }
}
