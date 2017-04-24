using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Bullet : IEntity, ICollidesWith<Building>, ICollidesWith<Enemy>, ICollidesWith<ShootingEnemy>, ICollidesWith<BombEnemy>, ICollidesWith<Player>
    {
		public Vector2 Position { get; set; }
		private Texture2D texture;
		public Rectangle Box { get; set; }
		public Vector2 Speed { get; private set; }
		public bool Alive { get; set; }
        public string Owner { get; private set; }
        private int damageToPlayer;
        private int damageToEnemy;

		public Bullet(Vector2 position, Vector2 speed, string owner)
		{
            Owner = owner;
			Position = position;
			Speed = speed;
			texture = TextureLoader.Bullet;
			Box = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
			Alive = true;
            damageToPlayer = 10;
            damageToEnemy = 15;
		}

		public void Collide(IEntity entity)
		{
            Destroy();
		}
        public void Collide(Building entity)
        {
            Destroy();
        }
        public void Collide (Player entity)
        {
            if (Owner != "Player")
            {
                entity.GetDamage(damageToPlayer);
                Destroy();
            }
           
        }

        public void Collide(Enemy entity)
        {
            if (Owner != "Enemy")
            {
                entity.GetDamage(damageToEnemy);
                Destroy();
            }
        }

        public void Collide(ShootingEnemy entity)
        {
            if (Owner != "Enemy")
            {
                entity.GetDamage(damageToEnemy);
                Destroy();
            }
        }

        public void Collide(BombEnemy entity)
        {
            if (Owner != "Enemy")
            {
                entity.GetDamage(damageToEnemy);
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
