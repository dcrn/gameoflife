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
			canvas.color = Color.RoyalBlue;

			LifeGrid life = new LifeGrid();
			life.color = Color.White;
			canvas.Add(life);
			life.FitToParent(2);
			life.Height -= 62;

			Panel brushes = new Panel();
			brushes.color = Color.CornflowerBlue;
			canvas.Add(brushes);
			brushes.FitToParent(2);
			brushes.Height = 60;
			brushes.Y = canvas.Height - 62;
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
		public override void Update(double deltatime, double totaltime)
		{
			canvas.Update(deltatime);
		}
	}
}
