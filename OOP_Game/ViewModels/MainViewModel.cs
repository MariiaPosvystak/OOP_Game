using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OOP_Game.Models;
using OOP_Game.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace OOP_Game.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly GameService _gameService = new();

        private readonly Stopwatch _stopwatch = new();

        // ===== GAME STATE =====

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
            selectedTheme = Themes[0];
            StartNewGame();
        }

        // ===== COMMANDS =====

        [RelayCommand]
        private void StartNewGame()
        {
            moves = 0;

            _gameService.Shuffle();

            tiles = new ObservableCollection<Tile>(_gameService.Tiles);

            _stopwatch.Restart();

            StartTimer();
        }

        [RelayCommand]
        private async Task TileTapped(Tile tile)
        {
            if (tile.IsEmpty)
                return;

            bool moved = _gameService.MoveTile(tile);

            if (!moved)
            {
                return;
            }

            Moves++;

            RefreshTiles();

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(50);
            });

            if (_gameService.IsSolved())
            {
                _stopwatch.Stop();
                SaveBestResult();

                await App.Current!.MainPage!.DisplayAlert(
                    "Победа 🎉",
                    $"Ходы: {Moves}\nВремя: {TimeText}",
                    "OK");
            }
        }

        [RelayCommand]
        private void ChangeTheme(Theme theme)
        {
            selectedTheme = theme;
        }

        // ===== HELPERS =====

        private void RefreshTiles()
        {
            tiles = new ObservableCollection<Tile>(_gameService.Tiles);
        }

        private async void StartTimer()
        {
            while (_stopwatch.IsRunning)
            {
                timeText = _stopwatch.Elapsed.ToString(@"mm\:ss");
                await Task.Delay(500);
            }
        }

        private void SaveBestResult()
        {
            int best = Preferences.Get("best_moves", int.MaxValue);

            if (moves < best)
            {
                Preferences.Set("best_moves", moves);
            }
        }
    }
}
