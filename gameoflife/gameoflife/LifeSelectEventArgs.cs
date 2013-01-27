using System;

namespace gameoflife
{
	class LifeSelectEventArgs : EventArgs
	{
		public bool[,] grid;

		public LifeSelectEventArgs(bool[,] grid)
			: base()
		{
			this.grid = grid;
		}
	}
}
