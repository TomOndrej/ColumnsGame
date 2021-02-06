using Xamarin.Forms.Xaml;

namespace ColumnsGame.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverlay
    {
        public GameOverlay()
        {
            this.IsVisible = false;

            InitializeComponent();
        }
    }
}