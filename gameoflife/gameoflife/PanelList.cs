using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameoflife
{
	class PanelList : Panel
	{
		int padding;

		public int Padding
		{
			get { return padding; }
			set { padding = value; }
		}

		public PanelList()
			: base()
		{
			padding = 0;
		}

		public override void Add(Panel chld)
		{
			children.Add(chld);
			chld.parent = this;

			int x = padding;

			// On add, re-arrange children
			foreach (Panel p in this.children)
			{
				p.X = x;
				x = x + p.Width + padding;
				p.CenterY();
			}
		}

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			base.Draw(spriteBatch, xoffset, yoffset);

			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}
	}
}
