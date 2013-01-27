using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
	class LifeGrid : Panel
	{
		// 2D array for holding cell data
		bool[,] grid1;
		bool[,] grid2;

		// Playing and speed settings
		bool playing;
		double speed;
		double lastUpdate;
		
		// Last mouse position on grid
		int lastgx;
		int lastgy;

		// Grid size
		int gridw;
		int gridh;

		// Cell size
		int cellSize;

		// Brush to be used
		Brush currentBrush;

		// Selection
		int selectMode;
		Point selectPoint;
		public EventHandler<LifeSelectEventArgs> OnSelect;

		public int SelectMode
		{
			get { return selectMode; }
			set { selectMode = value; }
		}

		public Brush CurrentBrush
		{
			get { return currentBrush; }
			set { currentBrush = value; }
		}

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
			cellSize = 0;

			lastgx = 0;
			lastgy = 0;

			playing = false;
			speed = 0.05;
			lastUpdate = 0.0;

			currentBrush = null;
			selectMode = 0;
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

		public void ResizeGrid(int w, int h)
		{
			bool[,] oldgrid1 = grid1;

			grid1 = new bool[w, h];
			grid2 = new bool[w, h];

			for (int x = 0; x < Math.Min(gridw, w); x++)
			{
				for (int y = 0; y < Math.Min(gridh, h); y++)
				{
					grid1[x, y] = oldgrid1[x, y];
				}
			}

			gridw = w;
			gridh = h;

			Width = w * cellSize;
			Height = h * cellSize;
		}

		bool CellAlive(int x, int y)
		{
			// Check if a cell should be alive next iteration

			// Count alive neighbors
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

			// Game rules
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

		public void Load()
		{
			// Load a board from file

			this.Playing = false;

			// New file select dialog
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

			// Try open file
			try
			{
				file = new StreamReader(filedialog.FileName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return;
			}

			// Clear board
			this.Clear();

			// Loop through data, output to grid
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
			// Save board

			// Open save file dialog
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

			// Write grid data to file
			for (int y = 0; y < gridh; y++)
			{
				for (int x = 0; x < gridw; x++)
				{
					file.Write(grid1[x, y] ? '1' : '0');
				}

				if (y != gridh - 1)
					file.Write(file.NewLine);
			}

			file.Close();
		}

		public void Clear()
		{
			// Clear the board
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
			// Randomize the board
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

		void UpdateBoard()
		{
			// Update the board (next iteration of game)
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

		// Called hooks
		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			base.Draw(spriteBatch, xoffset, yoffset);

			// Draw game grid
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

			// If selecting (Point 1)
			if (selectMode == 1)
			{
				MouseState mouse = Mouse.GetState();
				int gx = (mouse.X - xoffset) / cellSize;
				int gy = (mouse.Y - yoffset) / cellSize;

				if (gx >= 0 && gy >= 0 && gx < gridw && gy < gridh)
				{
					spriteBatch.Draw(Game1.instance.blankTexture,
						new Rectangle(
							xoffset + gx * cellSize, yoffset + gy * cellSize,
							cellSize, cellSize
							),
							new Color(255, 0, 0, 100));
				}
			}
			// If selecting point 2
			else if (selectMode == 2)
			{
				MouseState mouse = Mouse.GetState();
				int gx = (mouse.X - xoffset) / cellSize;
				int gy = (mouse.Y - yoffset) / cellSize;

				if (gx >= selectPoint.X && gy >= selectPoint.Y)
				{
					spriteBatch.Draw(Game1.instance.blankTexture,
						new Rectangle(
							xoffset + selectPoint.X * cellSize, yoffset + selectPoint.Y * cellSize,
							(gx - selectPoint.X + 1) * cellSize, (gy - selectPoint.Y + 1) * cellSize
							),
							new Color(255, 0, 0, 100));
				}
			}

			// If a brush is selected, draw it where the mouse is
			else if (currentBrush != null)
			{
				// Get mouse grid position
				MouseState mouse = Mouse.GetState();
				int gx = (mouse.X - xoffset) / cellSize;
				int gy = (mouse.Y - yoffset) / cellSize;

				// Color for drawing the brush
				Color translucent = new Color(0, 0, 0, 120);

				// Only draw if the mouse is inside the grid
				if (gx >= 0 && gy >= 0 && gx < gridw && gy < gridh)
				{
					// Loop through brush grid
					for (int x = 0; x < currentBrush.gridw; x++)
					{
						for (int y = 0; y < currentBrush.gridh; y++)
						{

							// Draw brush cells if they're inside the panel
							if ((gx + x) < gridw && (gy + y) < gridh &&
								(gx + x) >= 0 && (gy + y) >= 0 &&
								currentBrush.grid[x, y])
							{
								// Draw each cell
								spriteBatch.Draw(Game1.instance.blankTexture,
									new Rectangle(
										xoffset + (x + gx) * cellSize, yoffset + (y + gy) * cellSize,
										cellSize - 1, cellSize - 1
										),
										translucent);
							}
						}
					}
				}
			}

			// Draw children
			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		public override void Update(double deltatime)
		{
			// If playing, update based on speed setting
			if (playing)
			{
				lastUpdate += deltatime;
				if (lastUpdate > speed)
				{
					UpdateBoard();
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

			// Only able to draw if brush isn't set and not selecting
			if (currentBrush == null && selectMode == 0)
			{
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
			}

			return true;
		}

		public override bool MousePressed(int button, int x, int y)
		{
			if (base.MousePressed(button, x, y))
			{
				return true;
			}

			// Reset last mouse grid coord
			lastgx = 0;
			lastgy = 0;

			// If the brush is set, use that to draw
			if (selectMode == 1)
			{
				// Store point
				selectPoint = new Point(x / cellSize, y / cellSize);

				// Next part of selecting
				selectMode++;
			}
			else if (currentBrush != null)
			{
				// Mouse position on grid
				int gx = x / cellSize;
				int gy = y / cellSize;

				// Loop through brush
				for (int brushx = 0; brushx < currentBrush.gridw; brushx++)
				{
					for (int brushy = 0; brushy < currentBrush.gridh; brushy++)
					{
						// Copy cell to grid if it's inside the Life grid and alive
						if ((gx + brushx) < gridw && (gy + brushy) < gridh &&
							(gx + brushx) >= 0 && (gy + brushy) >= 0 &&
							currentBrush.grid[brushx, brushy])
						{
							grid1[gx + brushx, gy + brushy] = button == 1;
						}
					}
				}
			}

			return true;
		}

		public override bool MouseReleased(int button, int x, int y)
		{
			if (base.MouseReleased(button, x, y))
			{
				return true;
			}

			// Reset last mouse grid coord
			lastgx = 0;
			lastgy = 0;

			// If selecting an area
			if (selectMode == 2)
			{
				int gx = x / cellSize;
				int gy = y / cellSize;

				int w = gx - selectPoint.X + 1;
				int h = gy - selectPoint.Y + 1;

				if (w < 0 || h < 0)
				{
					selectMode = 0;
					return true;
				}

				bool[,] selection = new bool[w, h];

				for (int sx = 0; sx < w; sx++)
				{
					for (int sy = 0; sy < h; sy++)
					{
						selection[sx, sy] = grid1[selectPoint.X + sx, selectPoint.Y + sy];
					}
				}

				selectMode = 0;
				OnSelect(this, new LifeSelectEventArgs(selection));
			}

			return true;
		}
	}
}
