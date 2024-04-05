using System;
using System.Numerics;

namespace Minesweeper
{
	public static class GameCreatorFactory
	{
		public static IGameManager CreateGame(MinesweeperGameSettings settings)
		{
			return new GameManager(settings.Size, settings.Mines);
		}

		public static IGameManager CreateGame(int size, int mines)
		{
			return new GameManager(size, mines);
		}

		public static IGameManager CreateGame(int size)
		{
			return new GameManager(size);
		}
	}
}

