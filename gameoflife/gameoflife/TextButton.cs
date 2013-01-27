using System;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class TextButton : Button
	{
		Label label;
		public TextButton()
			: base()
		{
			label = new Label();
			this.Add(label);
		}

		public String Text
		{
			set
			{
				label.Text = value;
				label.SizeToContents();
				label.Center();
			}
			get
			{
				return label.Text;
			}
		}

		public SpriteFont Font
		{
			set
			{
				label.Font = value;
				label.SizeToContents();
				label.Center();
			}
			get
			{
				return label.Font;
			}
		}

		public new int Width
		{
			set
			{
				rect.Width = value;
				label.CenterX();
			}
			get
			{
				return rect.Width;
			}
		}

		public new int Height
		{
			set
			{
				rect.Height = value;
				label.CenterY();
			}
			get
			{
				return rect.Height;
			}
		}
	}
}
