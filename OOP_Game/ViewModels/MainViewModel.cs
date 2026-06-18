using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OOP_Game.Models;
using System.Collections.ObjectModel;

namespace OOP_Game.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // ======================
        // GAME CORE
        // ======================
        private readonly Game _game = new();

        // ======================
        // UI STATE
        // ======================

        [ObservableProperty]
        private ObservableCollection<Tile> tiles = new();

        [ObservableProperty]
        private int moves;

        [ObservableProperty]
        private string timeText = "00:00";

        [ObservableProperty]
        private Player player = new("Player");

        [ObservableProperty]
        private Theme selectedTheme;

        public List<Theme> Themes => GameTheme.Themes;

        public MainViewModel()
        {
            SelectedTheme = Themes[0];
            StartNewGame();
            StartTimer();
        }


        [RelayCommand]
        private void StartNewGame()
        {
            _game.Start();
            Moves = 0;

            Tiles.Clear();
            foreach (var t in _game.Tiles)
                Tiles.Add(t);
             RefreshTiles();
        }

        [RelayCommand]
        private async Task TileTapped(Tile tile)
        {
            if (tile.IsEmpty)
                return;

            bool moved = _game.MoveTile(tile);

            if (!moved)
                return;

            Moves++;

            RefreshTiles();

            if (_game.IsSolved())
            {
                _game.Stop();

                SaveBestResult();

                await App.Current!.MainPage!.DisplayAlert(
                    "Win 🎉",
                    _game.GetResult(),
                    "OK");
            }
        }


        [RelayCommand]
        private void ChangeTheme(Theme theme)
        {
            SelectedTheme = theme;
            theme.Apply(App.Current!.MainPage!);
        }


        private void RefreshTiles()
        {
            Tiles.Clear();

            foreach (var t in _game.Tiles)
                Tiles.Add(t);
        }

        private async void StartTimer()
        {
            while (true)
            {
                if (_game.IsRunning)
                    TimeText = _game.TimeText;

                await Task.Delay(500);
            }
        }

        private void SaveBestResult()
        {
            int best = Preferences.Get("best_moves", int.MaxValue);

            if (Moves < best)
                Preferences.Set("best_moves", Moves);
        }
    }
}