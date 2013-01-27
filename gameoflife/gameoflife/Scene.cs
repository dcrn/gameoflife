using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace gameoflife
{
	public abstract class Scene
	{
		public ContentManager Content;
		public Scene()
		{
			Content = new ContentManager(Game1.instance.Services);
			Content.RootDirectory = "Content";
		}
		~Scene()
		{
			Content.Unload();
		}
		public abstract void Initialize();
		public abstract void MousePressed(int button, int x, int y);
		public abstract void MouseReleased(int button, int x, int y);
		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void MouseDown(int button, int x, int y, double deltatime);
		public abstract void Update(double deltatime, double totaltime);
		public abstract void WindowResized();
	}
}
