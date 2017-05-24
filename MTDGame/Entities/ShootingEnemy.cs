using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MTDGame.Components;

namespace MG
{
    public class ShootingEnemy : ICollidesWith<Building>, ICollidesWith<ShootingEnemy>, ICollidesWith<Player>, ICollidesWith<Enemy>, ICollidesWith<BombEnemy>, IComponentEntity
    {
        private float wrongRotation = 0;
        public float AttackTimer { get; private set; }
        private float shootDistance;
        private Vector2 spriteOrigin;
        public Vector2 moveVector;
        public Vector2 distance;
        public bool Alive { get; set; }
        public int Attack { get; set; }
        public float rotation = 0;
        public float Rotation { get { return rotation; } set { rotation = value; } }
        public int bulletSpeed;

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

        public ShootingEnemy(Vector2 startPosition)
        {            
            Attack = 10;
            Alive = true;
            bulletSpeed = 4;
            Components = new Dictionary<Type, IComponent>
            {
                {Type.GetType("MG.Position"), new Position(this, startPosition)},
                {Type.GetType("MG.Movement"), new Movement(this, new Vector2(5,5))},
                {Type.GetType("MG.Collidable"), new Collidable(this, startPosition, TextureLoader.Enemy)},
                {Type.GetType("MG.Health"), new Health(this, 100)},
                {Type.GetType("MG.Transform"), new Transform(this, 0)},
                {Type.GetType("MG.Visible"), new Visible(this, TextureLoader.Enemy)}
            };
        }

        public void Update(GameTime gameTime)
        {
            shootDistance = Vector2.Distance(Game1.mainPlayer.GetComponent<Position>().position, GetComponent<Position>().position);
            GetComponent<Collidable>().Update();
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            AttackTimer -= timer;
            spriteOrigin = new Vector2(GetComponent<Visible>().Texture.Width, GetComponent<Visible>().Texture.Height) / 2;
            if ((shootDistance < 500) && (shootDistance > GetComponent<Visible>().Texture.Width) && (shootDistance > GetComponent<Visible>().Texture.Height))
                Shoot();
            Follow();
        }

        public void Shoot()
        {
            if (AttackTimer <= 0)
            {
                Random bulletRotation = new Random();
                wrongRotation = (float)Math.Atan2(distance.Y + bulletRotation.Next(-30, 30), distance.X + bulletRotation.Next(-30, 30));
                AttackTimer = 1f;
                var bullet = new Bullet(GetComponent<Position>().position, new Vector2((float)Math.Cos(wrongRotation), (float)Math.Sin(wrongRotation)) * bulletSpeed,
                    this.GetType(), Attack);
                EntityManager.Add(bullet);
            }
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

        public void Collide(Player entity)
        {
            CalculateCollide(entity);
            if (AttackTimer <= 0)
            {
                entity.GetComponent<Health>().GetDamage(Attack);
                AttackTimer = 0.9f;
            }
        }

        public void Collide(ShootingEnemy entity)
        {
            if (entity != this) CalculateCollide(entity);
        }

        public void Collide(BombEnemy entity)
        {
            CalculateCollide(entity);
        }


        public void Collide(Enemy entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Building entity)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<Visible>().Texture, GetComponent<Position>().position, null, Color.White,
                             GetComponent<Transform>().Rotation + (float)(Math.PI * 0.5f),
                             spriteOrigin, 1f, SpriteEffects.None, 0);
        }
    }
}
