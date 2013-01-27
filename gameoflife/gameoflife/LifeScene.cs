using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace gameoflife
{
	class LifeScene : Scene
	{
		Panel canvas;
		Panel lifeBg;
		LifeGrid life;
		PanelList controlmenu;
		PanelList brushmenu;
		SelectList brushlist;

		public override void Initialize()
		{
			// Canvas for the scene's gui elements
			canvas = new Panel();
			canvas.Width = Game1.instance.GraphicsDevice.Viewport.Width;
			canvas.Height = Game1.instance.GraphicsDevice.Viewport.Height;
			canvas.Color = Color.RoyalBlue;

			// Center background for Life
			lifeBg = new Panel();
			lifeBg.Color = Color.CornflowerBlue;
			lifeBg.Width = canvas.Width - 4;
			lifeBg.Height = canvas.Height - 30 - 50 - 8;
			lifeBg.X = 2;
			lifeBg.Y = 34;
			canvas.Add(lifeBg);

			// Game of Life panel
			life = new LifeGrid();
			life.Color = Color.WhiteSmoke;
			life.CellSize = 7;
			life.SetGridSize(lifeBg.Width / life.CellSize, lifeBg.Height / life.CellSize);
			lifeBg.Add(life);
			life.Center();
			// If an area is selected, save it as a brush
			life.OnSelect += SaveBrush;

			// Menu panel
			controlmenu = new PanelList();
			controlmenu.Padding = 2;
			controlmenu.Color = Color.CornflowerBlue;
			controlmenu.Width = canvas.Width - 4;
			controlmenu.Height = 30;
			controlmenu.X = 2;
			controlmenu.Y = 2;
			canvas.Add(controlmenu);

			// Clear board button
			TextButton clear = new TextButton();
			clear.Text = "Clear";
			clear.Color = Color.RoyalBlue;
			clear.Width = 85;
			clear.Height = 26;
			clear.OnClick += delegate { life.Clear(); };
			controlmenu.Add(clear);

			// Randomize board button
			TextButton random = new TextButton();
			random.Text = "Randomize";
			random.Color = Color.RoyalBlue;
			random.Width = 85;
			random.Height = 26;
			random.OnClick += delegate { life.Random(); };
			controlmenu.Add(random);

			// Load board button
			TextButton load = new TextButton();
			load.Text = "Load";
			load.Color = Color.RoyalBlue;
			load.Width = 85;
			load.Height = 26;
			load.OnClick += delegate { life.Load(); };
			controlmenu.Add(load);

			// Save board button
			TextButton save = new TextButton();
			save.Text = "Save";
			save.Color = Color.RoyalBlue;
			save.Width = 85;
			save.Height = 26;
			save.OnClick += delegate { life.Save(); };
			controlmenu.Add(save);

			// Play button
			SpriteButton play = new SpriteButton();
			play.Load("play");
			play.Color = Color.RoyalBlue;
			play.Width = 26;
			play.Height = 26;
			play.OnClick += delegate { life.Playing = true; };
			controlmenu.Add(play);

			// Pause button
			SpriteButton pause = new SpriteButton();
			pause.Load("pause");
			pause.Color = Color.RoyalBlue;
			pause.Width = 26;
			pause.Height = 26;
			pause.OnClick += delegate { life.Playing = false; };
			controlmenu.Add(pause);

			Label fastText = new Label();
			fastText.Text = "Speed: fast ";
			fastText.SizeToContents();
			controlmenu.Add(fastText);

			// Speed slider
			Slider speed = new Slider();
			speed.Color = Color.RoyalBlue;
			speed.Width = 100;
			speed.Height = 14;
			speed.OnChange += delegate { life.Speed = Math.Max(speed.Value, 0.03); };
			controlmenu.Add(speed);

			Label slowText = new Label();
			slowText.Text = " slow";
			slowText.SizeToContents();
			controlmenu.Add(slowText);

			// Brush menu panel
			brushmenu = new PanelList();
			brushmenu.Padding = 2;
			brushmenu.Color = Color.CornflowerBlue;
			brushmenu.Width = canvas.Width - 4;
			brushmenu.Height = 50;
			brushmenu.X = 2;
			brushmenu.Y = canvas.Height - 52;
			canvas.Add(brushmenu);

			// Brush load button
			SpriteButton loadBrush = new SpriteButton();
			loadBrush.Load("loadbrush");
			loadBrush.Color = Color.RoyalBlue;
			loadBrush.Width = 46;
			loadBrush.Height = 46;
			loadBrush.OnClick += LoadBrush;
			brushmenu.Add(loadBrush);

			// New brush button
			SpriteButton newBrush = new SpriteButton();
			newBrush.Load("savebrush");
			newBrush.Color = Color.RoyalBlue;
			newBrush.Width = 46;
			newBrush.Height = 46;
			newBrush.OnClick += delegate { life.SelectMode = life.SelectMode > 0 ? 0 : 1; };
			brushmenu.Add(newBrush);
			
			// Brush selection list
			brushlist = new SelectList();
			brushlist.Padding = 2;
			brushlist.Height = 50;
			brushlist.Width = brushmenu.Width - 46 * 2 - 2 * 4;
			brushlist.OnChange += delegate { life.CurrentBrush = (Brush)brushlist.selected; };
			brushmenu.Add(brushlist);

			// Default Life speed value
			speed.Value = 0.03;
			life.Speed = 0.03;
		}

		// Event from LifeGrid
		void SaveBrush(object sender, LifeSelectEventArgs args)
		{
			// Save dialog
			SaveFileDialog savedialog = new SaveFileDialog();
			savedialog.CheckPathExists = true;
			savedialog.Filter = "Game of Life brush (*.lifebrush)|*.lifebrush";
			savedialog.FilterIndex = 0;

			DialogResult result = savedialog.ShowDialog();
			if (result != DialogResult.OK)
				return;

			// Create brush from selected grid
			Brush b = new Brush();
			b.SetGrid(args.grid);
			b.Width = 46;
			b.Height = 46;
			brushlist.Add(b);

			b.Save(savedialog.FileName);
		}

		// OnClick from load
		void LoadBrush(object sender, EventArgs args)
		{
			// Open a select file dialog
			OpenFileDialog filedialog = new OpenFileDialog();
			filedialog.CheckPathExists = true;
			filedialog.CheckFileExists = true;
			filedialog.Multiselect = true;
			filedialog.Filter = "Game of life brush (*.lifebrush)|*.lifebrush";
			filedialog.FilterIndex = 0;

			DialogResult result = filedialog.ShowDialog();
			if (result != DialogResult.OK)
				return;

			// Loop through all files selected, create brushes and add them
			foreach (String file in filedialog.FileNames)
			{
				Brush b = new Brush();
				if (b.Load(file))
				{
					b.Width = 46;
					b.Height = 46;
					brushlist.Add(b);
				}
			}
		}

		public override void WindowResized()
		{
			canvas.Width = Game1.instance.GraphicsDevice.Viewport.Width;
			canvas.Height = Game1.instance.GraphicsDevice.Viewport.Height;

			lifeBg.Width = canvas.Width - 4;
			lifeBg.Height = canvas.Height - 30 - 50 - 8;
			lifeBg.X = 2;
			lifeBg.Y = 34;

			controlmenu.Width = canvas.Width - 4;
			controlmenu.Height = 30;
			controlmenu.X = 2;
			controlmenu.Y = 2;

			brushmenu.Width = canvas.Width - 4;
			brushmenu.Height = 50;
			brushmenu.X = 2;
			brushmenu.Y = canvas.Height - 52;

			life.ResizeGrid(lifeBg.Width / life.CellSize, lifeBg.Height / life.CellSize);
			life.Center();
		}

		public override void MousePressed(int button, int x, int y)
		{
			canvas.MousePressed(button, x, y);
		}

		public override void MouseReleased(int button, int x, int y)
		{
			canvas.MouseReleased(button, x, y);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			canvas.Draw(spriteBatch, 0, 0);
		}

		public override void MouseDown(int button, int x, int y, double deltatime)
		{
			canvas.MouseDown(button, x, y, deltatime);
		}

		public override void Update(double deltatime, double totaltime)
		{
			canvas.Update(deltatime);
		}
	}
}
