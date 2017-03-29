using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public interface IEntity
	{
		Vector2 Position { get; set; }
		Rectangle Box { get; set; }
		bool Alive { get; set; }
		void Collide(IEntity entity);
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);

	}
}
