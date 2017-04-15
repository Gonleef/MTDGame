using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Building : IEntity
	{
		public Vector2 Position { get; set; }
		public Rectangle Box { get; set; }
		private Texture2D texture;
		private Vector2 position;
		public bool Alive { get; set; }

		public Building(Vector2 startPosition)
		{
			position = startPosition;
			texture = TextureLoader.Building;
			Box = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
			Alive = true;
		}

		public void Collide(IEntity entity)
		{

		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, null, Color.White);
		}
	}
}
