using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Enemy : BaseEnemy, ICollidesWith<Building>, ICollidesWith<Enemy>, ICollidesWith<Player>
	{
		public float AttackTimer { get; private set; }

		public Enemy(Vector2 startPosition)
		{
			texture = TextureLoader.Enemy;
			Position = startPosition;

			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
			                    texture.Width, texture.Height);
			Alive = true;
			Health = 100;
			Attack = 10;
            speed = 4;
		}

		public override void Update(GameTime gameTime)
		{
			var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
			AttackTimer -= timer;
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
								texture.Width, texture.Height);
			spriteOrigin = new Vector2(texture.Width, texture.Height) / 2; 
			Follow();
		}

        public void Collide(Player entity)
        {
            //CalculateCollide(entity);
			if (AttackTimer <= 0)
			{
				entity.GetComponent<Health>().GetDamage(Attack);
				AttackTimer = 0.3f;
			}
        }

        public void Collide(ShootingEnemy entity)
        {
            //CalculateCollide(entity);
        }

        public void Collide(BombEnemy entity)
        {
           // CalculateCollide(entity);
        }

        public void Collide(Enemy entity)
        {
          //  if (this != entity) CalculateCollide(entity);
        }
    }
}
