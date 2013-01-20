using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class Button : Panel
	{
		Color hovercolor;
		public EventHandler ClickEvent;

		public Button()
			: base()
		{
			hovercolor = new Color(0, 0, 0, 40);
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			base.Draw(spriteBatch, xoffset, yoffset);

			MouseState m = Mouse.GetState();
			if (m.X > xoffset && m.Y > yoffset && m.X < (xoffset + this.Width) && m.Y < (yoffset + this.Height))
			{
				spriteBatch.Draw(Game1.instance.blankTexture,
					new Rectangle(
						xoffset, yoffset,
						this.Width, this.Height
						),
					hovercolor);
			}

			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		public override bool MousePressed(int button, int x, int y)
		{
			return base.MousePressed(button, x, y);
		}

		public override bool MouseReleased(int button, int x, int y)
		{
			// Check if there is a child panel that should be clicked first
			if (base.MouseReleased(button, x, y))
				return true;

			// No child panel has been clicked, so this button's click event should be called
			ClickEvent(this, new EventArgs());
			return true;
		}
	}
}
