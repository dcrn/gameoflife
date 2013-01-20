using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	public abstract class Scene
	{
		public abstract void Initialize();
		public abstract void LoadContent();
		public abstract void UnloadContent();
		public abstract void MousePressed(int button, int x, int y);
		public abstract void MouseReleased(int button, int x, int y);
		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void Update(double deltatime, double totaltime);
	}
}
