using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MTDGame.Components;

namespace MG
{
	public class Player : ICollidesWith<Building>, IComponentEntity
	{
		public Camera PlayerCamera { get; set; }
		private Vector2 spriteOrigin;
		public bool Alive { get; set; }
        public string shootType = "Player";

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

        public void Initialize(Vector2 PlayerPosition)
        {
			PlayerCamera = new Camera(PlayerPosition);
			Alive = true;
            Weapon playerWeapon = new Shotgun(this);
            Components = new Dictionary<Type, IComponent>
	        {
		        {Type.GetType("MG.Position"), new Position(this, PlayerPosition)},
		        {Type.GetType("MG.Movement"), new Movement(this, new Vector2(5,5))},
                {Type.GetType("MG.Health"), new Health(this, 100)},
		        {Type.GetType("MG.HasInventory"), new HasInventory(this)},
                {Type.GetType("MG.HasWeapon"), new HasWeapon(this, playerWeapon)},
                {Type.GetType("MG.Transform"), new Transform(this, 0)},
		        {Type.GetType("MG.Visible"), new Visible(this, TextureLoader.Player)},
                {Type.GetType("MG.PlayerScore"), new PlayerScore(this, 0)},
                {Type.GetType("MG.Collidable"), new Collidable(this, PlayerPosition, TextureLoader.Player)}
            };
	        Weapon pistol = new Pistol(this);
	        GetComponent<HasInventory>().Add<Weapon>(pistol);
        }

		public void Update(GameTime gameTime)
		{
			GetComponent<Collidable>().Update();
            spriteOrigin = new Vector2(GetComponent<Visible>().Texture.Width, GetComponent<Visible>().Texture.Height) / 2;
            PlayerCamera.Update(gameTime, (int)GetComponent<Position>().position.X , (int)GetComponent<Position>().position.Y);
			GetComponent<HasWeapon>().Update(gameTime);
		}

		public void Collide(IComponentEntity entity)
		{
           CalculateCollide(entity);
        }

        public void Collide(Building entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Enemy entity)
        {
            CalculateCollide(entity);
        }


        public void CalculateCollide(IComponentEntity entity)
        {
            var entityBoxCenterX = GetComponent<Collidable>().Box.Center.X;
            var entityBoxCenterY = GetComponent<Collidable>().Box.Center.Y;
            var entityBoxSizeX = GetComponent<Collidable>().Box.Size.X;
            var entityBoxSizeY = GetComponent<Collidable>().Box.Size.Y;
            var entityBoxLeft = GetComponent<Collidable>().Box.Left;
            var entityBoxRight = GetComponent<Collidable>().Box.Right;

            var subTopPoint = new Vector2(entityBoxCenterX, entityBoxCenterY - entityBoxSizeY / 2);
            var subLeftPoint = new Vector2(entityBoxCenterX - entityBoxSizeX / 2, entityBoxCenterY);
            var subBottomPoint = new Vector2(entityBoxCenterX, entityBoxCenterY + entityBoxSizeY / 2);
            var subRightPoint = new Vector2(entityBoxCenterX + entityBoxSizeX / 2, entityBoxCenterY);

            var topPoint = (int)Vector2.Distance(GetComponent<Position>().position, subTopPoint);
            var leftPoint = (int)Vector2.Distance(GetComponent<Position>().position, subLeftPoint);
            var bottomPoint = (int)Vector2.Distance(GetComponent<Position>().position, subBottomPoint);
            var rightPoint = (int)Vector2.Distance(GetComponent<Position>().position, subRightPoint);

            var minDistance = Math.Min(topPoint, Math.Min(leftPoint, Math.Min(bottomPoint, rightPoint)));

            if (topPoint == minDistance)
                GetComponent<Position>().position = new Vector2(GetComponent<Position>().position.X, GetComponent<Collidable>().Box.Top - GetComponent<Collidable>().Size.Y / 2);
            if (leftPoint == minDistance)
                GetComponent<Position>().position = new Vector2(GetComponent<Collidable>().Box.Left - GetComponent<Collidable>().Size.X / 2, GetComponent<Position>().position.Y);
            if (bottomPoint == minDistance)
                GetComponent<Position>().position = new Vector2(GetComponent<Position>().position.X, GetComponent<Collidable>().Box.Bottom + GetComponent<Collidable>().Size.Y / 2);
            if (rightPoint == minDistance)
                GetComponent<Position>().position = new Vector2(GetComponent<Collidable>().Box.Right + GetComponent<Collidable>().Size.X / 2, GetComponent<Position>().position.Y);
        }


		public Matrix GetCameraMatrix()
		{
			return PlayerCamera.transformMatrix;
		}

		public void Draw(SpriteBatch spriteBatch)
		{

			spriteBatch.Draw(GetComponent<Visible>().Texture, GetComponent<Position>().position, null, Color.White,
			                 GetComponent<Transform>().Rotation + (float)(Math.PI * 0.5f),
			                 spriteOrigin, 1f, SpriteEffects.None, 0);
			GetComponent<HasWeapon>().Draw(spriteBatch, GetComponent<HasWeapon>().Texture, GetComponent<Position>().position,
				GetComponent<Transform>().Rotation + (float)(Math.PI * 0.5f), spriteOrigin);
		}
	}
}