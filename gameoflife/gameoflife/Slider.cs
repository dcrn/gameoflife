using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class Slider : Panel
	{
		public EventHandler OnChange;
		double value;

		public double Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		public Slider()
			: base()
		{
			value = 0.0;
		}

		public override void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			spriteBatch.Draw(Game1.instance.blankTexture,
				new Rectangle(
					xoffset, yoffset + this.Height / 2 - 2,
					Width, 4
					),
				Color);

			spriteBatch.Draw(Game1.instance.blankTexture,
				new Rectangle(
					xoffset + (int)(this.Width * value) - 2, yoffset,
					4, this.Height
					),
				Color);
		}

		public override bool MouseDown(int button, int x, int y, double deltatime)
		{
			value = (double)x / (double)Width;
			OnChange(this, new EventArgs());
			return true;
		}
	}
}
