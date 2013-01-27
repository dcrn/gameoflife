using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class MenuScene : Scene
	{
		Panel canvas;
		Label title;
		Panel menubox;

		public override void Initialize()
		{
			// Background
			canvas = new Panel();
			canvas.Color = Color.RoyalBlue;
			canvas.Width = Game1.instance.GraphicsDevice.Viewport.Width;
			canvas.Height = Game1.instance.GraphicsDevice.Viewport.Height;

			// Title text
			title = new Label();
			title.Text = "Conway's Game of Life";
			title.SizeToContents();
			title.Y = 40;
			canvas.Add(title);
			title.CenterX();

			// Menu area
			menubox = new Panel();
			menubox.Color = Color.CornflowerBlue;
			menubox.Width = 200;
			menubox.Height = 190;
			canvas.Add(menubox);
			menubox.Center();

			// Play button
			TextButton playButton = new TextButton();
			playButton.Font = Game1.instance.titlefont;
			playButton.Text = "Play";
			playButton.Width = 180;
			playButton.Height = 80;
			playButton.X = 10;
			playButton.Y = 10;

			// Exit button
			TextButton exitButton = new TextButton();
			exitButton.Font = Game1.instance.titlefont;
			exitButton.Text = "Exit";
			exitButton.Width = 180;
			exitButton.Height = 80;
			exitButton.X = 10;
			exitButton.Y = 100;

			// Add buttons to menu box
			menubox.Add(playButton);
			menubox.Add(exitButton);

			// Button events
			playButton.OnClick += delegate { Play(); };
			exitButton.OnClick += delegate { Game1.instance.Exit(); };
		}

		public void Play()
		{
			// Change scene to LifeScene
			Scene life = new LifeScene();
			Game1.instance.ChangeScene(life);
		}

		public override void WindowResized()
		{
			canvas.Width = Game1.instance.GraphicsDevice.Viewport.Width;
			canvas.Height = Game1.instance.GraphicsDevice.Viewport.Height;

			menubox.Center();
			title.CenterX();
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
