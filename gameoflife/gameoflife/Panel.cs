using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gameoflife
{
	class Panel
	{
		private Color color;

		public Panel parent;
		protected List<Panel> children;
		protected Rectangle rect;

		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		public int X
		{
			get { return rect.X; }
			set { rect.X = value; }
		}

		public int Y
		{
			get { return rect.Y; }
			set { rect.Y = value; }
		}

		public int Width
		{
			get { return rect.Width; }
			set { rect.Width = value; }
		}

		public int Height
		{
			get { return rect.Height; }
			set { rect.Height = value; }
		}

		public Point Middle
		{
			get { return rect.Center; }
		}

		public Panel()
		{
			this.color = Color.Transparent;
			this.rect = new Rectangle();
			this.children = new List<Panel>();
		}

		// Add a child element
		public virtual void Add(Panel p)
		{
			// Add a new child
			this.children.Add(p);
			p.parent = this;
		}

		// Draw this panel
		public virtual void Draw(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			spriteBatch.Draw(Game1.instance.blankTexture,
				new Rectangle(
					xoffset, yoffset,
					this.Width, this.Height
					),
				this.color);
			
			// Only draw children automatically if this is a Panel (not a derived type)
			if (this.GetType() == typeof(Panel))
				this.DrawChildren(spriteBatch, xoffset, yoffset);
		}

		// Draw children panels
		public virtual void DrawChildren(SpriteBatch spriteBatch, int xoffset, int yoffset)
		{
			foreach (Panel p in children)
			{
				p.Draw(spriteBatch, xoffset + p.X, yoffset + p.Y);
			}
		}

		// Update hook, call update on children
		public virtual void Update(double deltatime)
		{
			// Loop through all children
			foreach (Panel p in children)
			{
				// Call update on child
				p.Update(deltatime);
			}
		}

		// Mouse pressed hook
		public virtual bool MousePressed(int button, int x, int y)
		{
			int locx, locy;

			// Loop through all children
			foreach (Panel p in children)
			{
				// x, y local to the child panel
				locx = x - p.X;
				locy = y - p.Y;

				// Check if it's in bounds of the child panel
				if (locx > 0 && locy > 0 && locx < p.Width && locy < p.Height &&
						// And check it's children
						p.MousePressed(button, locx, locy)
					)
				{
					// Return true to tell that something has been clicked
					return true;
				}
			}
			return false;
		}

		public virtual bool MouseDown(int button, int x, int y, double deltatime)
		{
			int locx, locy;

			// Loop through all children
			foreach (Panel p in children)
			{
				// x, y local to the child panel
				locx = x - p.X;
				locy = y - p.Y;

				// Check if it's in bounds of the child panel
				if (locx > 0 && locy > 0 && locx < p.Width && locy < p.Height &&
					// And check it's children
						p.MouseDown(button, locx, locy, deltatime)
					)
				{
					// Return true to tell that something has been clicked
					return true;
				}
			}
			return false;
		}

		// Mouse released hook
		public virtual bool MouseReleased(int button, int x, int y)
		{
			int locx, locy;

			// Loop through all children
			foreach (Panel p in children)
			{
				// x, y local to the child panel
				locx = x - p.X;
				locy = y - p.Y;

				// Check if it's in bounds of the child panel
				if (locx > 0 && locy > 0 && locx < p.Width && locy < p.Height &&
					// And check it's children
						p.MouseReleased(button, locx, locy)
					)
				{
					// Return true to tell that something has been clicked
					return true;
				}
			}
			return false;
		}

		// Convenience functions
		// Center this panel in relation to the parent
		public virtual void Center()
		{
			if (this.parent != null)
			{
				this.X = this.parent.Width / 2 - this.Width / 2;
				this.Y = this.parent.Height / 2 - this.Height / 2;
			}
		}

		public virtual void CenterX()
		{
			if (this.parent != null)
			{
				this.X = this.parent.Width / 2 - this.Width / 2;
			}
		}

		public virtual void CenterY()
		{
			if (this.parent != null)
			{
				this.Y = this.parent.Height / 2 - this.Height / 2;
			}
		}

		// Resize this panel to only fit the contents
		public virtual void SizeToContents()
		{
			int maxwidth = 0;
			int maxheight = 0;

			foreach (Panel p in children)
			{
				if ((p.X + p.Width) > maxwidth)
					maxwidth = p.X + p.Width;
				if ((p.Y + p.Height) > maxheight)
					maxheight = p.Y + p.Height;
			}
			this.Width = maxwidth;
			this.Height = maxheight;
		}

		// Resize to fit the parent, with a margin
		public virtual void FitToParent(int margin)
		{
			if (this.parent != null)
			{
				this.Width = this.parent.Width - margin * 2;
				this.Height = this.parent.Height - margin * 2;

				this.X = margin;
				this.Y = margin;
			}
		}
	}
}
