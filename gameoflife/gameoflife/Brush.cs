using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace gameoflife
{
	class Brush : Panel
	{
		public bool[,] grid;
		public int gridw;
		public int gridh;

		public Brush()
			: base()
		{
			gridw = 0;
			gridh = 0;
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			// Draw cells, cell size based on height/width of this
			// No spacing
			if (grid != null)
			{
				// Find out optimal cell width and height
				int cellw = Width / gridw;
				int cellh = Height / gridh;

				// Center
				xoffset += Width / 2 - cellw * gridw / 2;
				yoffset += Height / 2 - cellh * gridh / 2;

				// Loop through grid
				for (int x = 0; x < gridw; x++)
				{
					for (int y = 0; y < gridh; y++)
					{
						// Draw each cell
						spriteBatch.Draw(Game1.instance.blankTexture,
							new Rectangle(
								xoffset + x * cellw, yoffset + y * cellh,
								cellw, cellh
								),
								grid[x, y] == true ? Color.Black : Color.White);
					}
				}
			}
		}

		public bool Load(String file)
		{
			StreamReader brushfile = new StreamReader(file);
			String data = brushfile.ReadToEnd();
			brushfile.Close();

			String[] newline = { "\r\n" };
			String[] brushlines = data.Split(newline, StringSplitOptions.None);

			this.gridw = 0;
			foreach (String str in brushlines)
			{
				if (str.Length > gridw)
				{
					this.gridw = str.Length;
				}
			}
			this.gridh = brushlines.Length;

			this.grid = new bool[this.gridw, this.gridh];

			for (int y = 0; y < brushlines.Length; y++)
			{
				for (int x = 0; x < brushlines[y].Length; x++)
				{
					grid[x, y] = brushlines[y][x] == '1';
				}
			}

			return true;
		}

		public bool Save(String file)
		{
			return false;
		}
	}
}
