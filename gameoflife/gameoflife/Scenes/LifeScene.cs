using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife.Scenes
{
	class LifeScene : Scene
	{
		Panel canvas;
		public override void Initialize()
		{
			canvas = new Panel();
			canvas.Width = Game1.instance.GraphicsDevice.Viewport.Width;
			canvas.Height = Game1.instance.GraphicsDevice.Viewport.Height;
			canvas.Color = Color.RoyalBlue;

			Panel lifeBg = new Panel();
			lifeBg.Color = Color.CornflowerBlue;
			lifeBg.Width = canvas.Width - 4;
			lifeBg.Height = canvas.Height - 30 - 50 - 8;
			lifeBg.X = 2;
			lifeBg.Y = 34;
			canvas.Add(lifeBg);

			LifeGrid life = new LifeGrid();
			life.Color = Color.WhiteSmoke;
			life.CellSize = 9;
			life.SetGridSize(lifeBg.Width / 9, lifeBg.Height / 9);
			lifeBg.Add(life);
			life.Center();

			PanelList menubuttons = new PanelList();
			menubuttons.Color = Color.CornflowerBlue;
			menubuttons.Width = canvas.Width - 4;
			menubuttons.Height = 30;
			menubuttons.X = 2;
			menubuttons.Y = 2;
			canvas.Add(menubuttons);

			TextButton clear = new TextButton();
			clear.Text = "Clear";
			clear.Color = Color.DeepSkyBlue;
			clear.Width = 85;
			clear.Height = 26;
			clear.ClickEvent += delegate { life.Clear(); };
			menubuttons.Add(clear);

			TextButton random = new TextButton();
			random.Text = "Randomize";
			random.Color = Color.DeepSkyBlue;
			random.Width = 85;
			random.Height = 26;
			random.ClickEvent += delegate { life.Random(); };

			menubuttons.Add(random);

			TextButton load = new TextButton();
			load.Text = "Load";
			load.Color = Color.DeepSkyBlue;
			load.Width = 85;
			load.Height = 26;
			load.ClickEvent += delegate { life.Load(); };
			menubuttons.Add(load);

			TextButton save = new TextButton();
			save.Text = "Save";
			save.Color = Color.DeepSkyBlue;
			save.Width = 85;
			save.Height = 26;
			save.ClickEvent += delegate { life.Save(); };
			menubuttons.Add(save);

			SpriteButton play = new SpriteButton();
			play.Load("play");
			play.Color = Color.DeepSkyBlue;
			play.Width = 26;
			play.Height = 26;
			play.ClickEvent += delegate { life.Playing = true; };
			menubuttons.Add(play);

			SpriteButton pause = new SpriteButton();
			pause.Load("pause");
			pause.Color = Color.DeepSkyBlue;
			pause.Width = 26;
			pause.Height = 26;
			pause.ClickEvent += delegate { life.Playing = false; };
			menubuttons.Add(pause);

			Slider speed = new Slider();
			speed.Color = Color.DeepSkyBlue;
			speed.Width = 100;
			speed.Height = 14;
			speed.OnChange += delegate { life.Speed = speed.Value; };
			menubuttons.Add(speed);

			PanelList brushes = new PanelList();
			brushes.Color = Color.CornflowerBlue;
			brushes.Width = canvas.Width - 4;
			brushes.Height = 50;
			brushes.X = 2;
			brushes.Y = canvas.Height - 52;
			canvas.Add(brushes);
		}
		public override void LoadContent()
		{

		}
		public override void UnloadContent()
		{

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
