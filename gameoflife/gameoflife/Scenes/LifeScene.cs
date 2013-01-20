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

			PanelList brushes = new PanelList();
			brushes.color = Color.CornflowerBlue;
			canvas.Add(brushes);
			brushes.FitToParent(2);
			brushes.Height = 60;
			brushes.Y = canvas.Height - 62;

			Random r = new Random();
			for (int i = 0; i < 6; i++)
			{
				Panel block = new Panel();
				block.color = new Color(r.Next(255), r.Next(255), r.Next(255));
				block.Width = 56;
				block.Height = 56;

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
		public override void Update(double deltatime, double totaltime)
		{
			canvas.Update(deltatime);
		}
	}
}
