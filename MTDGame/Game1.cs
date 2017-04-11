using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace MG
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        Player mainPlayer = new Player();
        Vector2 backgroundPosition;
        Building building;

        public static Game1 Instance { get; private set; }
		public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
		public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

		IO io;

		public Game1()
		{
			Instance = this;
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
			Content.RootDirectory = "Content";

        }

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			base.Initialize();
			IsMouseVisible = true;
			EntityManager.Add(mainPlayer);
			EntityManager.Add(building);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			TextureLoader.LoadContent(Content);

			mainPlayer.Initialize(Game1.ScreenSize / 2);
			building = new Building(new Vector2(500, 500));

			//backgroundPosition = new Vector2(0, 0);
			io = new IO(mainPlayer);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ && !__TVOS__
						if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
							Exit();
			#endif

			// TODO: Add your update logic here
			io.Update(Keyboard.GetState(), Mouse.GetState(), gameTime);
			EntityManager.Update(gameTime);
			EnemyController.Update(gameTime);
			CollisionComtroller.Update();
			base.Update(gameTime);


		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			//TODO: Add your drawing code here
			spriteBatch.Begin(SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				null, null, null, null,
			    mainPlayer.GetCameraMatrix()
			);
			spriteBatch.Draw(TextureLoader.Background, backgroundPosition, Color.White);
			EntityManager.Draw(spriteBatch);
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}