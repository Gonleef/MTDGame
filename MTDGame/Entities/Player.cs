using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Player : IEntity, ICollidesWith<Building>
	{
		public Vector2 Position { get; set; }
		private Texture2D texture;
		public Rectangle Box { get; set; }
		public Camera PlayerCamera { get; set; }
		public Vector2 Size { get { return new Vector2(texture.Width, texture.Height); } }
		private float rotation = 0;
		private Vector2 spriteOrigin;
		public bool Alive { get; set; }
		private float shotTimer = 0;
		private int Health;
        public string shootType = "Player";

        public void Initialize(Vector2 PlayerPosition)
		{
			PlayerCamera = new Camera(PlayerPosition);
			Position = PlayerPosition;
			texture = TextureLoader.Player;
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.X / 2,
			                    texture.Width, texture.Height);
			Alive = true;
			Health = 10000;
		}

		public void Update(GameTime gameTime)
		{
			var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.X / 2,
			                    texture.Width, texture.Height);
			spriteOrigin = Size / 2;
			shotTimer -= timer;
			PlayerCamera.Update(gameTime, (int)Position.X, (int)Position.Y);
		}

		public void Shoot()
		{
			if (shotTimer <= 0)
			{
				var bullet = new Bullet(Position, new Vector2((float)Math.Cos(rotation),
															  (float)Math.Sin(rotation)) * 15, shootType);
				EntityManager.Add(bullet);
				shotTimer = 0.2f;
			}
		}

		public void GetDamage(int damage)
		{
			Health -= damage;
			if (Health <= 0)
				Alive = false;
            Console.WriteLine(Health);
        }

		public void Collide(IEntity entity)
		{
            CalculateCollide(entity);
        }

        public void Collide(Building entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(BombEnemy entity)
        {
            
        }

        public void Collide(Enemy entity)
        {
        }

        public void Collide(ShootingEnemy entity)
        {
        }

        public void CalculateCollide(IEntity entity)
        {
            var topPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X, entity.Box.Center.Y - entity.Box.Size.Y / 2));
            var leftPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X - entity.Box.Size.X / 2, entity.Box.Center.Y));
            var bottomPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X, entity.Box.Center.Y + entity.Box.Size.Y / 2));
            var rightPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X + entity.Box.Size.X / 2, entity.Box.Center.Y));

            var minDistance = Math.Min(topPoint, Math.Min(leftPoint, Math.Min(bottomPoint, rightPoint)));

            if (topPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Top - Size.Y / 2);
            if (leftPoint == minDistance) Position = new Vector2(entity.Box.Left - Size.X / 2, Position.Y);
            if (bottomPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Bottom + Size.Y / 2);
            if (rightPoint == minDistance) Position = new Vector2(entity.Box.Right + Size.X / 2, Position.Y); //Здесь
        }
        public void Move(Vector2 move)
        {
				Position += move;
		}

		public void Rotate(float rotation)
		{
			this.rotation = rotation;
		}

		public Matrix GetCameraMatrix()
		{
			return PlayerCamera.transformMatrix;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, null, Color.White,
			                 rotation + (float)(Math.PI * 0.5f),
			                 spriteOrigin, 1f, SpriteEffects.None, 0);
		}

	}
}