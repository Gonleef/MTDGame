﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Bullet : IEntity, ICollidesWith<Building>, ICollidesWith<Enemy>
    {
		public Vector2 Position { get; set; }
		private Texture2D texture;
		public Rectangle Box { get; set; }
		public Vector2 Speed { get; private set; }
		public bool Alive { get; set; }

		public Bullet(Vector2 position, Vector2 speed)
		{
			Position = position;
			Speed = speed;
			texture = TextureLoader.Bullet;
			Box = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
			Alive = true;
		}

		public void Collide(IEntity entity)
		{
            if (Box.Intersects(entity.Box)) Destroy();
		}
        public void Collide(Building entity)
        {
            if (Box.Intersects(entity.Box)) Destroy();
        }

        public void Collide(Enemy entity)
        {
            if (Box.Intersects(entity.Box))
            {
                entity.Health -= 25;
                if (entity.Health <= 0) entity.Alive = false;
                Destroy();
            }
        }

        public void Update(GameTime gameTime)
		{
			Position += Speed;
			Box = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
		}

		public void Destroy()
		{
			Alive = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, null, Color.White);
		}
	}
}
