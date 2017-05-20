using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MG
{
	public class Player : IEntity, ICollidesWith<Building>, IComponentEntity
	{
		public Vector2 Position { get; set; }
		private Texture2D texture;
		public Rectangle Box { get; set; }
		public Camera PlayerCamera { get; set; }
		public Vector2 Size { get { return new Vector2(texture.Width, texture.Height); } }
		public float rotation = 0;
		public float Rotation { get { return rotation; } set { rotation = value; } }
		private Vector2 spriteOrigin;
		public bool Alive { get; set; }
		private int Health;
        public string shootType = "Player";

        public Dictionary<Type, IComponent> Components { get; private set; }
		public T GetComponent<T>(Type component)
            where T:IComponent
		{
            if (HasComponent(component))
            {
               return (T)Components[component];
            }
            return default(T);
		}

		public bool HasComponent(Type component)
		{
			return true;
		}

        public void Initialize(Vector2 PlayerPosition)
        {
			PlayerCamera = new Camera(PlayerPosition);
			texture = TextureLoader.Player;
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.X / 2,
			                    texture.Width, texture.Height);
			Alive = true;
	        Health = 100;
            Weapon playerWeapon = new Shotgun((IEntity)this);
            Components = new Dictionary<Type, IComponent>
	        {
		        {Type.GetType("MG.Movement"), new Movement(PlayerPosition)},
                {Type.GetType("MG.Health"), new Health(Health)},
                {Type.GetType("MG.HasWeapon"), new HasWeapon(playerWeapon)},
                {Type.GetType("MG.Transform"), new Transform(Rotation)}
	        };
	        Position = PlayerPosition;
            
        }

		public void Update(GameTime gameTime)
		{
			Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.X / 2,
			                    texture.Width, texture.Height);
			spriteOrigin = Size / 2;
			PlayerCamera.Update(gameTime, (int)Position.X , (int)Position.Y);
			Inventory.activeWeapon.Update(gameTime);
		}

		public void Shoot()
		{
			Inventory.activeWeapon.Shoot();
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


        public void CalculateCollide(IEntity entity)
        {

            var subTopPoint = new Vector2(entity.Box.Center.X, entity.Box.Center.Y - entity.Box.Size.Y / 2);
            var subLeftPoint = new Vector2(entity.Box.Center.X - entity.Box.Size.X / 2, entity.Box.Center.Y);
            var subBottomPoint = new Vector2(entity.Box.Center.X, entity.Box.Center.Y + entity.Box.Size.Y / 2);
            var subRightPoint = new Vector2(entity.Box.Center.X + entity.Box.Size.X / 2, entity.Box.Center.Y);

            var topPoint = (int)Vector2.Distance(Position, subTopPoint);
            var leftPoint = (int)Vector2.Distance(Position, subLeftPoint);
            var bottomPoint = (int)Vector2.Distance(Position, subBottomPoint);
            var rightPoint = (int)Vector2.Distance(Position, subRightPoint);

            var minDistance = Math.Min(topPoint, Math.Min(leftPoint, Math.Min(bottomPoint, rightPoint)));

            if (topPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Top - Size.Y / 2);
            if (leftPoint == minDistance) Position = new Vector2(entity.Box.Left - Size.X / 2, Position.Y);
            if (bottomPoint == minDistance) Position = new Vector2(Position.X, entity.Box.Bottom + Size.Y / 2);
            if (rightPoint == minDistance) Position = new Vector2(entity.Box.Right + Size.X / 2, Position.Y);
        }

        /*public void Move(Vector2 move)
        {
			Position += move;
		}*/

		public void Rotate(float rotation)
		{
			this.Rotation = rotation;
		}

		public Matrix GetCameraMatrix()
		{
			return PlayerCamera.transformMatrix;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, null, Color.White,
			                 Rotation + (float)(Math.PI * 0.5f),
			                 spriteOrigin, 1f, SpriteEffects.None, 0);
		}

	}
}