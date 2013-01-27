using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gameoflife
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		MouseState prevMouseState;

		public static Game1 instance;
		public Scene activeScene;
		public Texture2D blankTexture;
		public SpriteFont titlefont;
		public SpriteFont textfont;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			instance = this;
		}

		protected override void Initialize()
		{
			// Make mouse visible
			this.IsMouseVisible = true;
			
			// Allow user resizing
			this.Window.AllowUserResizing = true;
			this.Window.ClientSizeChanged += WindowResized;

			// Load content etc
			base.Initialize();

			// Set active scene to menu
			ChangeScene(new MenuScene());
		}

		protected void WindowResized(object sender, EventArgs args)
		{
			activeScene.WindowResized();
		}

		public void ChangeScene(Scene s)
		{
			activeScene = s;
			activeScene.Initialize();
		}

		protected override void LoadContent()
		{
			// New sprite batch
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load fonts
			titlefont = Content.Load<SpriteFont>("TitleFont");
			textfont = Content.Load<SpriteFont>("TextFont");

			// New white 1x1 texture, used for drawing rectangles etc
			blankTexture = new Texture2D(GraphicsDevice, 1, 1);
			blankTexture.SetData(new Color[] { Color.White });
		}

		protected override void UnloadContent()
		{
			activeScene = null;
			Content.Unload();
		}

		protected override void Update(GameTime gameTime)
		{
			MouseState curMouse = Mouse.GetState();

			if (this.IsActive)
			{
				// Generate mouse events
				// Left mouse press/release
				if (curMouse.LeftButton != prevMouseState.LeftButton)
				{
					if (curMouse.LeftButton == ButtonState.Pressed)
						this.activeScene.MousePressed(1, curMouse.X, curMouse.Y);
					else
						this.activeScene.MouseReleased(1, curMouse.X, curMouse.Y);
				}
				// Right mouse press/release
				if (curMouse.RightButton != prevMouseState.RightButton)
				{
					if (curMouse.RightButton == ButtonState.Pressed)
						this.activeScene.MousePressed(2, curMouse.X, curMouse.Y);
					else
						this.activeScene.MouseReleased(2, curMouse.X, curMouse.Y);
				}
			}

			// Update the active scene
			activeScene.Update(gameTime.ElapsedGameTime.TotalSeconds, gameTime.TotalGameTime.TotalSeconds);

			if (this.IsActive)
			{
				if (curMouse.LeftButton == ButtonState.Pressed)
				{
					activeScene.MouseDown(1, curMouse.X, curMouse.Y, gameTime.ElapsedGameTime.TotalSeconds);
				}
				if (curMouse.RightButton == ButtonState.Pressed)
				{
					activeScene.MouseDown(2, curMouse.X, curMouse.Y, gameTime.ElapsedGameTime.TotalSeconds);
				}
			}

			prevMouseState = curMouse;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
				activeScene.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
