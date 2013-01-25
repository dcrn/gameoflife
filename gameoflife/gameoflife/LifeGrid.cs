using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Windows.Forms;

namespace gameoflife
{
	class LifeGrid : Panel
	{
		// 2D array for holding cell data
		bool[,] grid1;
		bool[,] grid2;

		// Playing and speed settings
		bool playing;
		double speed;
		double lastUpdate;
		
		// Last position on the grid for drawing
		int lastgx;
		int lastgy;

		// Grid size
		int gridw;
		int gridh;

		// Cell size
		int cellSize;

		public int CellSize
		{
			get { return cellSize; }
			set { cellSize = value; }
		}

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
			gridw = 0;
			gridh = 0;
			CellSize = 0;

			lastgx = 0;
			lastgy = 0;

			playing = false;
			speed = 0.1;
			lastUpdate = 0.0;
		}

		public void SetGridSize(int w, int h)
		{
			gridw = w;
			gridh = h;
			grid1 = new bool[w, h];
			grid2 = new bool[w, h];

			Width = w * cellSize;
			Height = h * cellSize;
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			base.Draw(spriteBatch, xoffset, yoffset);

			// Loop through grid
			for (int x = 0; x < gridw; x++)
			{
				for (int y = 0; y < gridh; y++)
				{
					// Draw each cell
					spriteBatch.Draw(Game1.instance.blankTexture,
						new Rectangle(
							xoffset + x * cellSize, yoffset + y * cellSize,
							cellSize - 1, cellSize - 1
							),
							grid1[x, y] == true ? Color.Black : Color.White);
				}
			}

			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		private bool CellAlive(int x, int y)
		{
			int alivecount = 0;
			for (int i = 0; i < 9; i++)
			{
				if (i != 4)
				{
					int locx = (i % 3) - 1;
					int locy = (i / 3) - 1;

					if ((x + locx) >= 0 && (y + locy) >= 0 &&
							(x + locx) < gridw && (y + locy) < gridh &&
							grid1[x + locx, y + locy])
					{
						alivecount++;
					}
				}
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

		private void updateBoard()
		{
			for (int x = 0; x < gridw; x++)
			{
				for (int y = 0; y < gridh; y++)
				{
					grid2[x, y] = CellAlive(x, y);
				}
			}

			// Swap arrays so grid2 is drawn now
			bool[,] tmp = grid2;
			grid2 = grid1;
			grid1 = tmp;
		}

		public override void Update(double deltatime)
		{
			if (playing)
			{
				lastUpdate += deltatime;
				if (lastUpdate > speed)
				{
					updateBoard();
					lastUpdate = 0.0;
				}
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
			int gx = x / cellSize;
			int gy = y / cellSize;

			// If the mouse has been moved, and it's a position in the grid
			if ((gx != lastgx || gy != lastgy) &&
				(gx >= 0 && gy >= 0 && gx < gridw && gy < gridh))
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

		public void Clear()
		{
			this.Playing = false;
			for (int x = 0; x < gridw; x++)
			{
				for (int y = 0; y < gridh; y++)
				{
					grid1[x, y] = false;
				}
			} 
		}

		public void Random()
		{
			this.Playing = false;
			Random r = new Random();
			for (int x = 0; x < gridw; x++)
			{
				for (int y = 0; y < gridh; y++)
				{
					grid1[x, y] = r.Next(2) == 1;
				}
			} 
		}

		public void Load()
		{
			this.Playing = false;
			OpenFileDialog filedialog = new OpenFileDialog();
			filedialog.CheckPathExists = true;
			filedialog.CheckFileExists = true;
			filedialog.Multiselect = false;
			filedialog.Filter = "Game of life board (*.life)|*.life";
			filedialog.FilterIndex = 0;

			DialogResult result = filedialog.ShowDialog();
			if (result != DialogResult.OK)
				return;

			StreamReader file;

			try
			{
				file = new StreamReader(filedialog.FileName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return;
			}

			this.Clear();

			int y = 0;
			while (!file.EndOfStream)
			{
				String line = file.ReadLine();

				for (int x = 0; x < line.Length; x++)
				{
					grid1[x, y] = line[x] == '1';
				}

				y++;
			}

			file.Close();
		}

		public void Save()
		{
			this.Playing = false;
			SaveFileDialog savedialog = new SaveFileDialog();
			savedialog.CheckPathExists = true;
			savedialog.Filter = "Game of Life board (*.life)|*.life";
			savedialog.FilterIndex = 0;

			DialogResult result = savedialog.ShowDialog();
			if (result != DialogResult.OK)
				return;

			StreamWriter file;

			try
			{
				file = new StreamWriter(savedialog.FileName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return;
			}

			for (int y = 0; y < gridh; y++)
			{
				for (int x = 0; x < gridw; x++)
				{
					file.Write(grid1[x, y] ? '1' : '0');
				}
				file.Write(file.NewLine);
			}

			file.Close();
		}
	}
}
