
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

		public new int Width
		{
			set
			{
				rect.Width = value;
				this.Rearrange();
			}
			get { return rect.Width; }
		}

		public new int Height
		{
			set
			{
				rect.Height = value;
				this.Rearrange();
			}
			get { return rect.Height; }
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

			Rearrange();
		}


		public void Rearrange()
		{
			// Re-arrange children
			int x = padding;
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
