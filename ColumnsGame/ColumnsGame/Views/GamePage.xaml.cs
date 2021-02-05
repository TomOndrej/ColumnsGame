using ColumnsGame.Converters;
using ColumnsGame.Ioc;
using ColumnsGame.Ioc.Attributes;
using ColumnsGame.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColumnsGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [IocRegisterNavigation]
    public partial class GamePage
    {
        public static readonly BindableProperty GameFieldDataProperty =
            BindableProperty.Create(nameof(GameFieldData), typeof(int[,]), typeof(GamePage),
                defaultBindingMode: BindingMode.OneWay, propertyChanged: OnGameFieldDataPropertyChanged);

        public GamePage()
        {
            InitializeComponent();

            this.SetBinding(GameFieldDataProperty, nameof(GamePageViewModel.GameFieldMatrix), BindingMode.OneWay);
        }

        private BoxView[,] GameFieldMatrix { get; set; }

        public int[,] GameFieldData
        {
            get => (int[,]) GetValue(GameFieldDataProperty);
            set => SetValue(GameFieldDataProperty, value);
        }

        private static void OnGameFieldDataPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
            {
                return;
            }

            var gameFieldData = (int[,]) newValue;
            var gamePage = (GamePage) bindable;

            if (gamePage.GameFieldMatrix == null)
            {
                gamePage.GenerateGameFieldMatrix(gameFieldData);
            }

            gamePage.ColorizeGameFieldMatrix(gameFieldData);
        }

        private void GenerateGameFieldMatrix(int[,] gameFieldData)
        {
            var fieldWidth = gameFieldData.GetUpperBound(0) + 1;
            var fieldHeight = gameFieldData.GetUpperBound(1) + 1;

            var columnDefinitions = new ColumnDefinitionCollection();

            for (var i = 0; i < fieldWidth; i++)
            {
                columnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});
            }

            this.GameFieldGrid.ColumnDefinitions = columnDefinitions;

            var rowDefinitions = new RowDefinitionCollection();

            for (var i = 0; i < fieldHeight; i++)
            {
                rowDefinitions.Add(new RowDefinition {Height = GridLength.Star});
            }

            this.GameFieldGrid.RowDefinitions = rowDefinitions;

            this.GameFieldMatrix = new BoxView[fieldWidth, fieldHeight];

            for (var i = 0; i < fieldWidth; i++)
            {
                for (var j = 0; j < fieldHeight; j++)
                {
                    var boxView = new BoxView();
                    boxView.SetValue(Grid.ColumnProperty, i);
                    boxView.SetValue(Grid.RowProperty, j);

                    this.GameFieldMatrix[i, j] = boxView;

                    this.GameFieldGrid.Children.Add(boxView);
                }
            }
        }

        private void ColorizeGameFieldMatrix(int[,] gameFieldData)
        {
            var converter = ContainerProvider.Resolve<BrickKindToColorConverter>();

            for (var i = 0; i <= gameFieldData.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= gameFieldData.GetUpperBound(1); j++)
                {
                    var targetKind = gameFieldData[i, j];

                    var targetColor = targetKind >= 0 ? converter.ConvertKind(targetKind) : Color.Default;

                    var boxView = this.GameFieldMatrix[i, j];

                    if (boxView.Color == targetColor)
                    {
                        continue;
                    }

                    boxView.Color = targetColor;
                }
            }
        }
    }
}