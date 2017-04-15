using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	public class Enemy : IEntity, ICollidesWith<Building>, ICollidesWith<Enemy>, ICollidesWith<Player>
    {
		public Vector2 Position { get; set; }
		private Texture2D texture;
		public Rectangle Box { get; set; }
		public Vector2 Size { get { return new Vector2(texture.Width, texture.Height); } }
		private float rotation = 0;
		public bool Alive { get; set; }
		public int Health { get; set; }
		private Vector2 spriteOrigin;

		public Enemy(Vector2 startPosition)
		{
			texture = TextureLoader.Enemy;
			Position = startPosition;

			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
			                    texture.Width, texture.Height);
			Alive = true;
			Health = 100;
		}

		public void Update(GameTime gameTime)
		{
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
								texture.Width, texture.Height);
			spriteOrigin = new Vector2(texture.Width, texture.Height) / 2; 
			Follow();
			
		}

		public void Move(Vector2 move)
		{
			Position += move;
		}

        public void Collide(IEntity entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Player entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Building entity)
        {
            CalculateCollide(entity);
        }
        public void Collide(Enemy entity)
        {
            if (entity!= this) CalculateCollide(entity);
        }

        private void CalculateCollide(IEntity entity)
        {
            var topPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X, entity.Box.Center.Y - entity.Box.Size.Y / 2));
            var leftPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X - entity.Box.Size.X / 2, entity.Box.Center.Y));
            var bottomPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X, entity.Box.Center.Y + entity.Box.Size.Y / 2));
            var rightPoint = (int)Vector2.Distance(Position, new Vector2(entity.Box.Center.X + entity.Box.Size.X / 2, entity.Box.Center.Y));

            var minDistance = Math.Min(topPoint, Math.Min(leftPoint, Math.Min(bottomPoint, rightPoint)));
            if (Box.Intersects(entity.Box))
            {
                if (topPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Top - Size.Y / 2);
                if (leftPoint == minDistance) Position = new Vector2(entity.Box.Left - Size.X / 2, Position.Y);
                if (bottomPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Bottom + Size.Y / 2);
                if (rightPoint == minDistance) Position = new Vector2(entity.Box.Right + Size.X / 2, Position.Y);
            }
        }

        public void Follow()
		{
			var distance = EntityManager.players[0].Position - Position;
			rotation = (float)Math.Atan2(distance.Y, distance.X);
			Move(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)));
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, null, Color.White,
						 rotation + (float)(Math.PI * 0.5f),
						 spriteOrigin, 1f, SpriteEffects.None, 0);
		}

    }
}
