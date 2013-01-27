using System;

namespace gameoflife
{
	class SpriteButton : Button
	{
		Sprite sprite;

		public new int Width
		{
			set
			{
				rect.Width = value;
				sprite.CenterX();
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
				sprite.CenterY();
			}
			get
			{
				return rect.Height;
			}
		}

		public SpriteButton()
			: base()
		{
			sprite = new Sprite();
			this.Add(sprite);
		}

		public void Load(String asset)
		{
			sprite.Load(asset);
			sprite.SizeToContents();
			sprite.Center();
		}
	}
}
