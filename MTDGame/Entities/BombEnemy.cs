using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MTDGame.Components;

namespace MG
{
    public class BombEnemy : ICollidesWith<Building>, ICollidesWith<BombEnemy>, 
        ICollidesWith<Player>, ICollidesWith<Enemy>, ICollidesWith<ShootingEnemy>, IComponentEntity
    {
        public float AttackTimer { get; private set; }
        private Vector2 spriteOrigin;
        public Vector2 moveVector;
        public Vector2 distance;
        public bool Alive { get; set; }
        public int Attack { get; set; }
        public float rotation = 0;
        public float Rotation { get { return rotation; } set { rotation = value; } }
        public Dictionary<Type, IComponent> Components { get; private set; }
        public T GetComponent<T>()
            where T : IComponent
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
        public BombEnemy(Vector2 startPosition)
        {
            Attack = 50;
            Alive = true;
            Components = new Dictionary<Type, IComponent>
            {
                {Type.GetType("MG.Position"), new Position(this, startPosition)},
                {Type.GetType("MG.Movement"), new Movement(this, new Vector2(5,5))},
                {Type.GetType("MG.Collidable"), new Collidable(this, startPosition, TextureLoader.BombEnemy)},
                {Type.GetType("MG.Health"), new Health(this, 100)},
                {Type.GetType("MG.Transform"), new Transform(this, 0)},
                {Type.GetType("MG.Visible"), new Visible(this, TextureLoader.BombEnemy)}
            };

        }

        public void Update(GameTime gameTime)
        {
            GetComponent<Collidable>().Update();
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            AttackTimer -= timer;
            spriteOrigin = new Vector2(GetComponent<Visible>().Texture.Width, GetComponent<Visible>().Texture.Height) / 2;
            Follow();
        }

        public virtual void Follow()
        {
            if (Game1.mainPlayer.Alive)
            {
                distance = Game1.mainPlayer.GetComponent<Position>().position - GetComponent<Position>().position;
                Rotation = (float)Math.Atan2(distance.Y, distance.X);
                GetComponent<Transform>().Rotate(Rotation);
                moveVector = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) / 4;
                GetComponent<Movement>().Move(moveVector);
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

        public void Collide(Player entity)
        {
            entity.GetComponent<Health>().GetDamage(Attack);
            Alive = false;
        }

        public void Collide(Building entity)
        {
            entity.GetComponent<Health>().GetDamage(Attack);
            Alive = false;
        }

        public void Collide(BombEnemy entity)
        {
            if (this != entity) CalculateCollide(entity);
        }

        public void Collide(Enemy entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(ShootingEnemy entity)
        {
            CalculateCollide(entity);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<Visible>().Texture, GetComponent<Position>().position, null, Color.White,
                             GetComponent<Transform>().Rotation + (float)(Math.PI * 0.5f),
                             spriteOrigin, 1f, SpriteEffects.None, 0);
        }
    }
}
