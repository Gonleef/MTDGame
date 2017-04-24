using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public class BombEnemy : BaseEnemy, ICollidesWith<Building>, ICollidesWith<BombEnemy>, ICollidesWith<Player>, ICollidesWith<Enemy>, ICollidesWith<ShootingEnemy>
    {
        public BombEnemy(Vector2 startPosition)
        {
            texture = TextureLoader.BombEnemy;
            Position = startPosition;

            Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
                                texture.Width, texture.Height);
            Alive = true;
            Health = 150;
            Attack = 50;
        }

        public override void Update(GameTime gameTime)
        {
            Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
                                texture.Width, texture.Height);
            spriteOrigin = new Vector2(texture.Width, texture.Height) / 2;
            Follow();
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
            entity.GetDamage(Attack);
            Alive = false;
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
            CalculateCollide(entity);
        }

        public void Collide(BombEnemy entity)
        {
            if (entity != this) CalculateCollide(entity);
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
