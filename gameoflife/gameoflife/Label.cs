using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class Label : Panel
	{
		String text;
		SpriteFont font;

		public String Text
		{
			get { return text; }
			set { text = value; }
		}

		public SpriteFont Font
		{
			get { return font; }
			set { font = value; }
		}

		public Label()
			: base()
		{
			// Defaults for text, color
			text = "";
			font = Game1.instance.textfont;
			color = Color.White;
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			// Don't draw any background

			// Draw text
			spriteBatch.DrawString(font, text, new Vector2(xoffset, yoffset), this.color);

			// Draw children.. if there are any..
			base.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		public override void SizeToContents()
		{
			// Set panel size to size of text instead
			Vector2 size = font.MeasureString(this.text);
			this.Width = (int)size.X;
			this.Height = (int)size.Y;
		}
	}
}
