using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class Sprite : Panel
	{
		Texture2D texture;
		public void Load(String name)
		{
			texture = Game1.instance.activeScene.Content.Load<Texture2D>(name);
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			spriteBatch.Draw(texture,
				new Rectangle(
					xoffset, yoffset,
					this.Width, this.Height
					),
				Color.White);
		}

		public override void SizeToContents()
		{
			this.Width = texture.Width;
			this.Height = texture.Height;
		}
	}
}
