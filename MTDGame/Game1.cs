using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace MG
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        public static Player mainPlayer = new Player();
        Vector2 backgroundPosition = Vector2.Zero;
        Building building;
        PlayerInfo playerInfo;

        public static Game1 Instance { get; private set; }
		public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
		public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static CollisionController collisionController { get; private set; }

        IO io;

		public Game1()
		{
			Instance = this;
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1920;
			graphics.PreferredBackBufferHeight = 1080;
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
			base.Initialize();
			IsMouseVisible = true;
            collisionController = new CollisionController();
            EntityManager.Add(mainPlayer);
			EntityManager.Add(building);
            playerInfo = new PlayerInfo(Content);

        }

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			TextureLoader.LoadContent(Content);            

			mainPlayer.Initialize(Game1.ScreenSize / 2);
			building = new Building(new Vector2(500, 500));

			io = new IO(mainPlayer);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			#if !__IOS__ && !__TVOS__
						if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
							Exit();
			#endif

			io.Update(Keyboard.GetState(), Mouse.GetState(), gameTime);
			EntityManager.Update(gameTime);
			EnemyController.Update(gameTime);
            collisionController.Update();
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				null, null, null, null,
			    mainPlayer.GetCameraMatrix()
			);
			spriteBatch.Draw(TextureLoader.Background, backgroundPosition, Color.White);
            playerInfo.DrawHealth(spriteBatch);             
            EntityManager.Draw(spriteBatch);
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}