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

		public override void Move(Vector2 move)
		{            
            base.Move(move);
		}

        public override void GetDamage(int damage)
        {
            base.GetDamage(damage);
        }

        public override void Collide(IEntity entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Player entity)
        {
            CalculateCollide(entity);
			if (AttackTimer <= 0)
			{
				entity.GetDamage(Attack);
				AttackTimer = 0.3f;
			}
        }

        public void Collide(Building entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Enemy entity)
        {
            if (entity!= this) CalculateCollide(entity);
        }

        public void Collide(ShootingEnemy entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(BombEnemy entity)
        {
            CalculateCollide(entity);
        }

        public override void CalculateCollide(IEntity entity)
        {
            base.CalculateCollide(entity);            
        }

        public override void Follow()
		{
            base.Follow();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
            base.Draw(spriteBatch);
		}

    }
}
