using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MG
{
	public class Building : IComponentEntity
	{
		public float rotation = 0;
		public float Rotation { get { return rotation; } set { rotation = value; } }
		public Vector2 Position { get; set; }
		public Rectangle Box { get; set; }
		private Texture2D texture;
		private Vector2 position;
		public bool Alive { get; set;}

		public Dictionary<Type, IComponent> Components { get; private set; }
		public T GetComponent<T>()
			where T:IComponent
		{
			if (HasComponent())
			{
				return (T)Components[typeof(T)];
			}
			return default(T);
		}

		public bool HasComponent()
		{
			return true;
		}

		public Building(Vector2 startPosition)
		{
			position = startPosition;
			texture = TextureLoader.Building;
			Box = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
			Alive = true;
		}

		public void Collide(IComponentEntity entity)
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
