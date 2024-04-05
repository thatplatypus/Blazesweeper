using System;
namespace Minesweeper.Models
{
	public class Tile
	{
		public bool IsRevealed { get; set; }

		public bool ShowDistance { get; set; }

		public bool IsMine { get; set; } = false;

		public bool HasFlagApplied { get; set; } = false;

		public int NeighboringMines { get;  set; }

		public int X { get; set; }

		public int Y { get; set; }
	}
}

