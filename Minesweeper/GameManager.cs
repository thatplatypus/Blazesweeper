using System.Numerics;
using Minesweeper.Models;

namespace Minesweeper
{
    public class GameManager : IGameManager
    {
        private int _size;
        private int _mines;
        private Tile[][] _board;
        private GameStatus _status = GameStatus.New;

        public Tile[][] Game { get => _board; }
        public MinesweeperGameSettings Settings { get; } = new();

        public GameManager(int size, int mines = 10)
        {
            _size = size;
            _mines = mines;
            Settings = new()
            {
                Mines = mines,
                Size = size,
            };
            _board = CreateGame(Settings);
            _status = GameStatus.Ongoing;
        }

        public Tile[][] Reset()
        {
            _board = CreateGame(new()
            {
                Mines = _mines,
                Size = _size
            });

            _status = GameStatus.Ongoing;

            return _board;
        }

        public GameStatus ClickTile(int x, int y)
        {
            if (_board[x][y].IsMine)
            {
                _board[x][y].IsRevealed = true;
                return _status = GameStatus.Lost;
            }

            RevealTile(x, y);

            if (AllNonMineTilesRevealed())
            {
                return _status = GameStatus.Won;
            }

            return _status = GameStatus.Ongoing;
        }

        private static Tile[][] CreateGame(MinesweeperGameSettings settings)
        {
            var board = new Tile[settings.Size][];
            var random = new Random();

            // Initialize the board with empty tiles
            for (int i = 0; i < settings.Size; i++)
            {
                board[i] = new Tile[settings.Size];
                for (int j = 0; j < settings.Size; j++)
                {
                    board[i][j] = new Tile()
                    {
                        X = i,
                        Y = j,
                    };
                }
            }

            // Randomly place mines
            for (int i = 0; i < settings.Mines; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(settings.Size);
                    y = random.Next(settings.Size);
                } while (board[x][y].IsMine);

                board[x][y].IsMine = true;
            }

            // Calculate neighboring mines for each tile
            for (int i = 0; i < settings.Size; i++)
            {
                for (int j = 0; j < settings.Size; j++)
                {
                    if (board[i][j].IsMine)
                        continue;

                    int mineCount = 0;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int nx = i + dx, ny = j + dy;
                            if (nx >= 0 && nx < settings.Size && ny >= 0 && ny < settings.Size && board[nx][ny].IsMine)
                                mineCount++;
                        }
                    }

                    board[i][j].NeighboringMines = mineCount;
                }
            }

            return board;
        }

        private void RevealTile(int x, int y)
        {
            if (_status == GameStatus.Lost)
                return;

            if (x < 0 || x >= _size || y < 0 || y >= _size || _board[x][y].IsRevealed)
            {
                return;
            }

            _board[x][y].IsRevealed = true;

            if (_board[x][y].NeighboringMines == 0)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        RevealTile(x + dx, y + dy);
                    }
                }
            }
        }

        private bool AllNonMineTilesRevealed()
        {
            return _board.All(row => row.All(tile => tile.IsMine || tile.IsRevealed));
        }

        public GameStatus GetStatus()
        {
            return _status;
        }

        public GameStatus RevealAll()
{
    foreach (var row in _board)
    {
        foreach (var tile in row)
        {
            tile.IsRevealed = true;
        }
    }

    return _status = GameStatus.Lost;
}
    }
}