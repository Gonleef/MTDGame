using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	class Player : IEntity
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

		public void Initialize(Vector2 PlayerPosition)
		{
			PlayerCamera = new Camera(PlayerPosition);
			Position = PlayerPosition;
			texture = TextureLoader.Player;
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.X / 2,
			                    texture.Width, texture.Height);
			Alive = true;
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
															  (float)Math.Sin(rotation)) * 15);
				EntityManager.Add(bullet);
				shotTimer = 0.2f;
			}
		}

		public void Collide(IEntity entity)
		{
			if (entity.GetType().Name == "Building" || entity.GetType().Name == "Enemy")
			{
				var topPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X, entity.Box.Center.Y - entity.Box.Size.Y / 2));
				var leftPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X - entity.Box.Size.X / 2, entity.Box.Center.Y));
				var bottomPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X, entity.Box.Center.Y + entity.Box.Size.Y / 2));
				var rightPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X + entity.Box.Size.X / 2, entity.Box.Center.Y));

				var minDistance = Math.Min(topPoint, Math.Min(leftPoint, Math.Min(bottomPoint, rightPoint)));

				if (topPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Top - Size.Y / 2);
				if (leftPoint == minDistance) Position = new Vector2(entity.Box.Left - Size.X / 2, Position.Y);
				if (bottomPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Bottom + Size.Y / 2);
				if (rightPoint == minDistance) Position = new Vector2(entity.Box.Right + Size.X / 2, Position.Y);
			}
		}

		public void Move(Vector2 move)
		{
		/*	var phantomBox = new Rectangle((int)(Position.X + move.X - Size.X / 2),
			                               (int)(Position.Y + move.Y - Size.Y / 2),
			                               (int)Size.X,
			                               (int)Size.Y);
			if (!CollisionComtroller.MoveCollision(phantomBox))
			{ */
				Position += move;
			//}
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