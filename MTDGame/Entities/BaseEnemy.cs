using System;
using System.Security;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public abstract class BaseEnemy: IComponentEntity
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get { return new Vector2(texture.Width, texture.Height); } }
        public Texture2D texture;
        public Rectangle Box { get; set; }
        public Vector2 spriteOrigin { get; set; }
        public Vector2 distance;
        public Vector2 moveVector;
        public bool Alive { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int speed = 1;
        public float rotation = 0;
        public float Rotation { get { return rotation; } set { rotation = value; } }

        public Dictionary<Type, IComponent> Components { get; set; }
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

        public virtual void Collide(IComponentEntity entity)
        {
            CalculateCollide(entity);
        }

        public virtual void Collide(Building entity)
        {
            CalculateCollide(entity);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Move(Vector2 move)
        {
            move *= speed;
            Position += move;
        }

        public virtual void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Alive = false;
                //Player.score += 1;
            }
        }

        public virtual void Follow()
        {
            if (Game1.mainPlayer.Alive)
            {
                distance = Game1.mainPlayer.GetComponent<Position>().position - Position;
                Rotation = (float)Math.Atan2(distance.Y, distance.X);
                moveVector = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) / 4;
                Move(moveVector);
            }
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White,
                Rotation + (float)(Math.PI * 0.5f),
                         spriteOrigin, 1f, SpriteEffects.None, 0);
        }
    }
}
