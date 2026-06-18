using System.Diagnostics;
using OOP_Game.Models;

namespace OOP_Game.Models;

public class Game
{
    public const int Size = 4;
    public List<Tile> Tiles { get; private set; } = new();

    private readonly Random _random = new();
    private readonly Stopwatch _stopwatch = new();

    public int Moves { get; private set; }
    public string TimeText => _stopwatch.Elapsed.ToString(@"mm\:ss");

    public bool IsRunning => _stopwatch.IsRunning;

    public Game()
    {
        Initialize();
    }


    public void Start()
    {
        Initialize();
        Shuffle();
        Moves = 0;
        _stopwatch.Restart(); 
    }

    public void Stop()
    {
        _stopwatch.Stop();
    }

    private void Initialize()
    {
        Tiles.Clear();

        int value = 1;

        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                if (row == Size - 1 && col == Size - 1)
                {
                    Tiles.Add(new Tile(0, row, col));
                }
                else
                {
                    Tiles.Add(new Tile(value++, row, col));
                }
            }
        }
    }
    public void Shuffle()
    {
        for (int i = 0; i < 300; i++)
        {
            var movable = GetMovableTiles();
            var tile = movable[_random.Next(movable.Count)];

            MoveTile(tile);
        }
    }

    public bool MoveTile(Tile tile)
    {
        var empty = Tiles.First(t => t.IsEmpty);

        bool canMove =
            (Math.Abs(tile.Row - empty.Row) == 1 && tile.Column == empty.Column) ||
            (Math.Abs(tile.Column - empty.Column) == 1 && tile.Row == empty.Row);

        if (!canMove)
            return false;

        (tile.Row, empty.Row) = (empty.Row, tile.Row);
        (tile.Column, empty.Column) = (empty.Column, tile.Column);

        Moves++;

        return true;
    }

    public List<Tile> GetMovableTiles()
    {
        var empty = Tiles.First(t => t.IsEmpty);

        return Tiles
            .Where(t => !t.IsEmpty &&
                ((Math.Abs(t.Row - empty.Row) == 1 && t.Column == empty.Column) ||
                 (Math.Abs(t.Column - empty.Column) == 1 && t.Row == empty.Row)))
            .ToList();
    }

    public bool IsSolved()
    {
        int expected = 1;

        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                var tile = Tiles.First(t => t.Row == row && t.Column == col);

                if (row == Size - 1 && col == Size - 1)
                    return tile.IsEmpty;

                if (tile.Value != expected++)
                    return false;
            }
        }

        return true;
    }

    public string GetResult()
    {
        return $"Käigud: {Moves}, Aeg: {TimeText}";
    }

    public void Reset()
    {
        Stop();
        Initialize();
        Moves = 0;
    }
}