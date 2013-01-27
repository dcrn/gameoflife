using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class SelectList : PanelList
	{
		public EventHandler OnChange;
		public Panel selected;

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			base.Draw(spriteBatch, xoffset, yoffset);

			if (selected != null)
			{
				// Draw gold outline underneath selected panel
				spriteBatch.Draw(Game1.instance.blankTexture,
					new Rectangle(
						xoffset + selected.X - 1, yoffset + selected.Y - 1,
						selected.Width + 2, selected.Height + 2
						),
						Color.Gold);
			}

			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		public override bool MousePressed(int button, int x, int y)
		{
			bool childclicked = base.MousePressed(button, x, y);
			int locx, locy;

			// Loop through all children
			foreach (Panel p in children)
			{
				// x, y local to the child panel
				locx = x - p.X;
				locy = y - p.Y;

				// Check if it's in bounds of the child panel
				if (locx > 0 && locy > 0 && locx < p.Width && locy < p.Height)
				{
					// Change selected panel
					if (p == this.selected)
					{
						this.selected = null;
					}
					else
					{
						this.selected = p;
					}

					// Call event
					OnChange(this, new EventArgs());
					return true;
				}
			}

			return childclicked;
		}
	}
}
