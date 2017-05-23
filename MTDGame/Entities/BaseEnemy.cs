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

        public virtual void Collide(IComponentEntity entity)
        {
            //CalculateCollide(entity);
        }

        public virtual void Collide(Building entity)
        {
            //CalculateCollide(entity);
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

      /*  public virtual void CalculateCollide(IComponentEntity entity)
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
*/
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White,
                Rotation + (float)(Math.PI * 0.5f),
                         spriteOrigin, 1f, SpriteEffects.None, 0);
        }
    }
}
