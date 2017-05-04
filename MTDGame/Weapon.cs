using System;
using Microsoft.Xna.Framework;

namespace MG
{
    public abstract class Weapon : IItem
    {
        private float reload;
        private float reloadTimer = 0;
        private float speed;
        private float speedTimer = 0;
        private int ammoSize;
        private int ammo;
        private float bulletSpeed;
        public IEntity Owner { get; set; }

        public Weapon(float reload, float speed, int ammoSize, float bulletSpeed, IEntity owner)
        {
            this.reload = reload;
            reloadTimer = reload;
            this.speed = speed;
            speedTimer = speed;
            this.ammoSize = ammoSize;
            ammo = ammoSize;
            this.bulletSpeed = bulletSpeed;
            this.Owner = owner;
        }

        public void Shoot()
        {
            if (speedTimer <= 0 && reloadTimer <= 0 && ammo > 0)
            {
                var bullet = new Bullet(Owner.Position, new Vector2((float)Math.Cos(Owner.Rotation),
                                                      (float)Math.Sin(Owner.Rotation)) * 15,
                                                        typeof(Player));
                EntityManager.Add(bullet);
                speedTimer = speed;
                ammo--;
            }
            else if (ammo <= 0 && reloadTimer <= 0)
            {
                Reload();
            }
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            speedTimer -= timer;
            if(reloadTimer > 0) reloadTimer -= timer;
            Console.WriteLine(Owner.Rotation);
        }

        public void Reload()
        {
            reloadTimer = reload;
            ammo = ammoSize;
        }

        public void Activate()
        {

        }
    }
}