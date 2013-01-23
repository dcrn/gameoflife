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

			Button pause = new Button();
			pause.Color = Color.DeepSkyBlue;
			pause.Width = 85;
			pause.Height = 26;
			pause.ClickEvent += delegate { life.Playing = !life.Playing; };

			menubuttons.Add(pause);

			for (int i = 0; i < 5; i++)
			{
				Panel block = new Button();
				block.Color = Color.DeepSkyBlue;
				block.Width = 85;
				block.Height = 26;

				menubuttons.Add(block);
			}

			PanelList brushes = new PanelList();
			brushes.Color = Color.CornflowerBlue;
			brushes.Width = canvas.Width - 4;
			brushes.Height = 50;
			brushes.X = 2;
			brushes.Y = canvas.Height - 52;
			canvas.Add(brushes);

			for (int i = 0; i < 6; i++)
			{
				Panel block = new Panel();
				block.Color = Color.DeepSkyBlue;
				block.Width = 46;
				block.Height = 46;

				brushes.Add(block);
			}
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
