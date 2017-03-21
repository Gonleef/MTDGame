using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Building : IEntity
	{
		public Rectangle Box { get; set; }
		private Texture2D texture;
		private Vector2 position;

		public Building(Vector2 startPosition)
		{
			position = startPosition;
			texture = TextureLoader.Building;
			Box = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
		}

		public void Collide(IEntity entity)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, null, Color.White);
		}
	}
}
