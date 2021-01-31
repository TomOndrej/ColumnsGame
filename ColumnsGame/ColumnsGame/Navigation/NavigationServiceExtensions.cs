using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Navigation;

namespace ColumnsGame.Navigation
{
    public static class NavigationServiceExtensions
    {
        public static async Task NavigateToAsync(
            this INavigationService navigationService, 
            string name, 
            NavigationParameters parameters = null, 
            bool? useModalNavigation = null, 
            bool animated = true)
        {
            INavigationResult navigationResult =
                await navigationService.NavigateAsync(name, parameters, useModalNavigation, animated);

            if (!navigationResult.Success)
            {
                ThrowNavigationException(navigationResult.Exception);
            }
        }

        [Conditional("DEBUG")]
        private static void ThrowNavigationException(Exception navigationException)
        {
            throw new Exception("Navigation exception", navigationException);
        }
    }
}
