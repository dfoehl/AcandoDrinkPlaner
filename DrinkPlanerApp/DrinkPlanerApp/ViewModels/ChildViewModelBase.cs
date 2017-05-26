namespace DrinkPlanerApp.ViewModels
{
    public abstract class ChildViewModelBase<T> : ViewModelBase where T : ViewModelBase
    {
        protected T ParentVM { get; private set; }

        public ChildViewModelBase(T parentVM) : base(parentVM.client)
        {
            this.ParentVM = parentVM;
        }
    }
}