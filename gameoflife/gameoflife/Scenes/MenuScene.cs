using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife.Scenes
{
	class MenuScene : Scene
	{
		Panel canvas;
		Label title;
		public override void Initialize()
		{
			canvas = new Panel();
			canvas.Color = Color.RoyalBlue;
			canvas.Width = Game1.instance.GraphicsDevice.Viewport.Width;
			canvas.Height = Game1.instance.GraphicsDevice.Viewport.Height;

			title = new Label();
			canvas.Add(title);

			title.Text = "Conway's Game of Life";
			title.SizeToContents();
			title.CenterX();
			title.Y = 40;

			Panel menubox = new Panel();
			menubox.Color = Color.CornflowerBlue;
			menubox.Width = 200;
			menubox.Height = 280;

			canvas.Add(menubox);
			menubox.Center();

			Button spButton = new Button();
			spButton.Width = 180;
			spButton.Height = 80;
			spButton.X = 10;
			spButton.Y = 10;

			Label spLabel = new Label();
			spLabel.Font = Game1.instance.titlefont;
			spLabel.Text = "Singleplayer";
			spLabel.SizeToContents();
			spButton.Add(spLabel);
			spLabel.Center();

			Button mpButton = new Button();
			mpButton.Width = 180;
			mpButton.Height = 80;
			mpButton.X = 10;
			mpButton.Y = 100;

			Label mpLabel = new Label();
			mpLabel.Font = Game1.instance.titlefont;
			mpLabel.Text = "Multiplayer";
			mpLabel.SizeToContents();
			mpButton.Add(mpLabel);
			mpLabel.Center();

			Button exitButton = new Button();
			exitButton.Width = 180;
			exitButton.Height = 80;
			exitButton.X = 10;
			exitButton.Y = 190;

			Label exitLabel = new Label();
			exitLabel.Font = Game1.instance.titlefont;
			exitLabel.Text = "Exit";
			exitLabel.SizeToContents();
			exitButton.Add(exitLabel);
			exitLabel.Center();

			menubox.Add(spButton);
			menubox.Add(mpButton);
			menubox.Add(exitButton);

			spButton.ClickEvent += delegate { Play(); };
			mpButton.ClickEvent += delegate { };
			exitButton.ClickEvent += delegate { Game1.instance.Exit(); };
		}
		public void Play()
		{
			Scene life = new Scenes.LifeScene();
			Game1.instance.ChangeScene(life);
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
			title.Y = 40 + (int)(Math.Sin(totaltime * 2.0) * 10.0);
		}
	}
}
