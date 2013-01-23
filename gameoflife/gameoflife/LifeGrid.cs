using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gameoflife
{
	class LifeGrid : Panel
	{
		// 2D array for holding cell data
		bool[,] curgrid;
		bool[,] grid1;
		bool[,] grid2;

		// Playing and speed settings
		bool playing;
		double speed;
		
		// Last position on the grid for drawing
		int lastgx;
		int lastgy;

		public bool Playing
		{
			get { return playing; }
			set { playing = value; }
		}

		public double Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		public LifeGrid()
			: base()
		{
			grid1 = new bool[88, 43];
			grid2 = new bool[88, 43];

			lastgx = 0;
			lastgy = 0;
			playing = false;
			speed = 1.0;
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			base.Draw(spriteBatch, xoffset, yoffset);

			// Loop through grid
			for (int x = 0; x < 88; x++)
			{
				for (int y = 0; y < 43; y++)
				{
					// Draw each cell
					spriteBatch.Draw(Game1.instance.blankTexture,
						new Rectangle(
							3 + xoffset + x * 9, 3 + yoffset + y * 9,
							8, 8
							),
							grid1[x, y] == true ? Color.Black : Color.White);
				}
			}

			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		private bool cellIsAlive(int x, int y)
		{
			if (x == 1 && y == 0)
			{
				Console.WriteLine(".");
			}
			int alivecount = 0;
			for (int i = 0; i < 9; i++)
			{
				if (i != 4)
				{
					int locx = (i % 3) - 1;
					int locy = (i / 3) - 1;

					if ((x + locx) >= 0 && (y + locy) >= 0 &&
							(x + locx) < 88 && (y + locy) < 43)
					{
						if (grid1[x + locx, y + locy])
						{
							alivecount++;
						}
					}
				}
			}
			if (x == 0 && y == 0)
			{
				Console.WriteLine("0, 0 alive neighbors: " + alivecount);
			}

			if (grid1[x, y])
			{
				if (alivecount < 2)
					return false;

				if (alivecount == 2 || alivecount == 3)
					return true;

				if (alivecount > 3)
					return false;
			}
			else if (alivecount == 3)
			{
				return true;
			}

			return false;
		}

		public override void Update(double deltatime)
		{
			if (playing)
			{
				for (int x = 0; x < 88; x++)
				{
					for (int y = 0; y < 43; y++)
					{
						grid2[x, y] = cellIsAlive(x, y);
					}
				}

				bool[,] tmp = grid2;
				grid2 = grid1;
				grid1 = tmp;
			}

			base.Update(deltatime);
		}

		public override bool MouseDown(int button, int x, int y, double deltatime)
		{
			if (base.MouseDown(button, x, y, deltatime))
			{
				return true;
			}

			// Grid position from mouse location
			int gx = (x - 3) / 9;
			int gy = (y - 3) / 9;

			// If the mouse has been moved, and it's a position in the grid
			if ((gx != lastgx || gy != lastgy) &&
				(gx >= 0 && gy >= 0 && gx < 88 && gy < 43))
			{
				// Set the cell on or off (on if left mouse button)
				grid1[gx, gy] = button == 1;

				lastgx = gx;
				lastgy = gy;
			}

			return true;
		}

		public override bool MousePressed(int button, int x, int y)
		{
			if (base.MousePressed(button, x, y))
			{
				return true;
			}

			lastgx = 0;
			lastgy = 0;

			return true;
		}

		public override bool MouseReleased(int button, int x, int y)
		{
			if (base.MouseReleased(button, x, y))
			{
				return true;
			}

			lastgx = 0;
			lastgy = 0;

			return true;
		}
	}
}
