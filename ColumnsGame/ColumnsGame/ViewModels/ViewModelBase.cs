using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public abstract class ViewModelBase : INavigatedAware
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
    }
}
