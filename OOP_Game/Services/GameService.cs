using OOP_Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Game.Services
{
    public class GameService
    {
        public const int Size = 4;

        private readonly Random _random = new();

        public List<Tile> Tiles { get; private set; } = [];

        public GameService()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
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
            InitializeBoard();
            for (int i = 0; i < 500; i++)
            {
                var movableTiles = GetMovableTiles();

                var tile =
                    movableTiles[_random.Next(movableTiles.Count)];

                MoveTile(tile);
            }
        }

        public bool MoveTile(Tile tile)
        {
            var emptyTile = Tiles.First(t => t.IsEmpty);

            bool canMove =
                (Math.Abs(tile.Row - emptyTile.Row) == 1 &&
                 tile.Column == emptyTile.Column)
                ||
                (Math.Abs(tile.Column - emptyTile.Column) == 1 &&
                 tile.Row == emptyTile.Row);

            if (!canMove)
                return false;

            (tile.Row, emptyTile.Row) =
                (emptyTile.Row, tile.Row);

            (tile.Column, emptyTile.Column) =
                (emptyTile.Column, tile.Column);

            return true;
        }

        public List<Tile> GetMovableTiles()
        {
            var emptyTile = Tiles.First(t => t.IsEmpty);

            return Tiles
                .Where(t =>
                    !t.IsEmpty &&
                    (
                        (Math.Abs(t.Row - emptyTile.Row) == 1 &&
                         t.Column == emptyTile.Column)
                        ||
                        (Math.Abs(t.Column - emptyTile.Column) == 1 &&
                         t.Row == emptyTile.Row)
                    ))
                .ToList();
        }

        public bool IsSolved()
        {
            int expected = 1;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    var tile =
                        Tiles.First(t =>
                            t.Row == row &&
                            t.Column == col);

                    if (row == Size - 1 &&
                        col == Size - 1)
                    {
                        return tile.Value == 0;
                    }

                    if (tile.Value != expected++)
                        return false;
                }
            }

            return true;
        }
    }
}
