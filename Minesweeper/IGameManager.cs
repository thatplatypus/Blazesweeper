using Minesweeper.Models;

namespace Minesweeper
{
    public interface IGameManager
    {
        public Tile[][] Reset();

        public Tile[][] Game { get; }

        public GameStatus ClickTile(int x, int y);

        public GameStatus GetStatus();

        public MinesweeperGameSettings Settings { get; }

        public GameStatus RevealAll();
    }
}