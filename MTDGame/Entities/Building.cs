using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MG
{
	public class Building : IComponentEntity
	{
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
            Alive = true;
            Components = new Dictionary<Type, IComponent>
            {
                {Type.GetType("MG.Position"), new Position(this, startPosition)},
                {Type.GetType("MG.Health"), new Health(this, 200)},
                {Type.GetType("MG.Visible"), new Visible(this, TextureLoader.Building)},
                {Type.GetType("MG.Collidable"), new Collidable(this, startPosition, TextureLoader.Building)}
            };
		}

		public void Collide(IComponentEntity entity)
		{

		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(GetComponent<Visible>().Texture, GetComponent<Position>().position, null, Color.White);
		}
	}
}
