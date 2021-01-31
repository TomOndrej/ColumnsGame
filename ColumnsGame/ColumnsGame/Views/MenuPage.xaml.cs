using ColumnsGame.Ioc.Attributes;
using Xamarin.Forms.Xaml;

namespace ColumnsGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [IocRegisterNavigation]
    public partial class MenuPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
    }
}