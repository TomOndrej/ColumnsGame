using ColumnsGame.Ioc.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColumnsGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [IocRegisterNavigation]
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }
    }
}