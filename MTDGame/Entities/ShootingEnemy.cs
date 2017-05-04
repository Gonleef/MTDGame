using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public class ShootingEnemy : BaseEnemy, ICollidesWith<Building>, ICollidesWith<ShootingEnemy>, ICollidesWith<Player>, ICollidesWith<Enemy>
    {
        private float wrongRotation = 0;
        private int bulletSpeed;
        public float AttackTimer { get; private set; }
        public string shootType = "Enemy";
        private float shootDistance;

        public ShootingEnemy(Vector2 startPosition)
        {
            texture = TextureLoader.ShootingEnemy;
            Position = startPosition;

            Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
                                texture.Width, texture.Height);
            Alive = true;
            Health = 100;
            Attack = 5;
            speed = 2;
            bulletSpeed = 8;
        }

        public override void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            AttackTimer -= timer;
            Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
                                texture.Width, texture.Height);
            spriteOrigin = new Vector2(texture.Width, texture.Height) / 2;
            shootDistance = Vector2.Distance(EntityManager.players[0].Position, Position);
            if ((shootDistance < 500) && (shootDistance > texture.Width) && (shootDistance > texture.Height))
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
                var bullet = new Bullet(Position, new Vector2((float)Math.Cos(wrongRotation), (float)Math.Sin(wrongRotation)) * bulletSpeed , this.GetType());
                EntityManager.Add(bullet);
            }
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
                AttackTimer = 0.9f;
            }
        }

        public void Collide(Building entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(Enemy entity)
        {
            CalculateCollide(entity);
        }

        public void Collide(ShootingEnemy entity)
        {
            if (entity != this) CalculateCollide(entity);
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
