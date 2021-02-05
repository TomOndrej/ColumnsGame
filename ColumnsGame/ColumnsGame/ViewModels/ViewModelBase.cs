using Prism.Mvvm;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigatedAware, IDestructible
    {
        protected INavigationService NavigationService { get; }

        protected ViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        { }

        public virtual void Destroy()
        { }
    }
}
