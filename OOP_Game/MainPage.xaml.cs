using Microsoft.Maui.Controls.Shapes;
using OOP_Game.ViewModels;

namespace OOP_Game;

public partial class MainPage : ContentPage
{
    private MainViewModel vm;

    public MainPage()
    {
        vm = new MainViewModel();

        BackgroundColor = vm.SelectedTheme.BackgroundColor;
        Title = "Viisteist";

        var root = new Grid
        {
            Padding = 20,
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star }
            }
        };
        var header = new VerticalStackLayout
        {
            Spacing = 5
        };

        var title = new Label
        {
            Text = "🧩 Viisteist",
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            TextColor = vm.SelectedTheme.TextColor,
            FontFamily = vm.SelectedTheme.FontFamily
        };

        var playerLabel = new Label
        {
            Text = vm.Player.Name,
            TextColor = vm.SelectedTheme.TextColor
        };

        var movesLabel = new Label
        {
            Text = $"Käigud: {vm.Moves}",
            TextColor = vm.SelectedTheme.TextColor
        };

        var timeLabel = new Label
        {
            Text = $"Aeg: {vm.TimeText}",
            TextColor = vm.SelectedTheme.TextColor
        };

        var themePicker = new Picker
        {
            Title = "Teema",
            ItemsSource = vm.Themes
        };
        themePicker.SelectedIndexChanged += (s, e) =>
        {
            if (themePicker.SelectedItem is Models.Theme theme)
            {
                themePicker.ItemDisplayBinding = new Binding("Name");
                vm.SelectedTheme = theme;
                theme.Apply(this); 
            }
        };

        var newGameButton = new Button
        {
            Text = "Uus mäng"
        };

        newGameButton.Clicked += (s, e) =>
        {
            vm.StartNewGameCommand.Execute(null);
        };

        header.Add(title);
        header.Add(playerLabel);
        header.Add(movesLabel);
        header.Add(timeLabel);
        header.Add(themePicker);
        header.Add(newGameButton);


        var tilesView = new CollectionView
        {
            ItemsSource = vm.Tiles,
            ItemsLayout =
                new GridItemsLayout(4, ItemsLayoutOrientation.Vertical)
        };
        tilesView.SelectionMode = SelectionMode.None;
        tilesView.ItemTemplate = new DataTemplate(() =>
        {
            var border = new Border
            {
                Padding = 0,
                Margin = new Thickness(3),
                StrokeShape = new RoundRectangle { CornerRadius = 12 },
            };
            border.SetBinding(Border.BackgroundColorProperty,
                new Binding("SelectedTheme.TileColor", source: vm));

            var label = new Label
            {
                FontSize = 26,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            label.SetBinding(Label.TextColorProperty,
                new Binding("SelectedTheme.TextColor", source: vm));
            label.SetBinding(Label.TextProperty, "DisplayValue");

            border.Content = label;

            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                if (((Element)s).BindingContext is Models.Tile tile)
                {
                    vm.TileTappedCommand.Execute(tile);
                }
            };
            border.GestureRecognizers.Add(tap);

            return border;
        });

        root.Add(header);
        root.Add(tilesView);

        Grid.SetRow(header, 0);
        Grid.SetRow(tilesView, 1);

        Content = root;
    }
}