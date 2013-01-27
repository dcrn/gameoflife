using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;

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

		public void SetGrid(bool[,] grid)
		{
			this.grid = grid;
			gridw = grid.GetLength(0);
			gridh = grid.GetLength(1);
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			if (grid != null)
			{
				// Find out optimal cell width and height
				int cellw = Width / gridw;
				int cellh = Height / gridh;

				// Center
				xoffset += Width / 2 - cellw * gridw / 2;
				yoffset += Height / 2 - cellh * gridh / 2;

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

		public bool Load(String filename)
		{
			// Try open the file
			StreamReader file;

			try
			{
				file = new StreamReader(filename);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}

			// Read all data from the file
			String data = file.ReadToEnd();
			file.Close();

			// Split data by newline into an array
			String[] newline = { "\r\n" };
			String[] brushlines = data.Split(newline, StringSplitOptions.None);

			// Find out the brush width and height
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

			// Set grid data from file data
			for (int y = 0; y < brushlines.Length; y++)
			{
				for (int x = 0; x < brushlines[y].Length; x++)
				{
					grid[x, y] = brushlines[y][x] == '1';
				}
			}

			return true;
		}

		public bool Save(String filename)
		{
			StreamWriter file;

			// Try open file for saving
			try
			{
				file = new StreamWriter(filename);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}

			// Write grid data to file
			for (int y = 0; y < gridh; y++)
			{
				for (int x = 0; x < gridw; x++)
				{
					file.Write(grid[x, y] ? '1' : '0');
				}

				if (y != gridh - 1)
					file.Write(file.NewLine);
			}

			file.Close();
			return true;
		}
	}
}
