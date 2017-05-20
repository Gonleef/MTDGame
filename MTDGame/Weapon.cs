using System;
using Microsoft.Xna.Framework;

namespace MG
{
    public abstract class Weapon : IItem
    {
        private float reload;
        private float reloadTimer = 0;
        public bool CanShoot = true;
        private float speed;
        private float speedTimer = 0;
        private int ammoSize;
        private int ammo;
        public bool NeedReload = false;
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

        public virtual void Shoot()
        {
            if (CanShoot)
            {
                CreateBullet(0);
                speedTimer = speed;
                ammo--;
                CanShoot = false;
            }
            else if (NeedReload)
            {
                Reload();
                NeedReload = false;
            }
        }

        public void CreateBullet(float angle)
        {
            var bullet = new Bullet(Owner.Position, new Vector2((float)Math.Cos(Owner.Rotation + angle),
                                                        (float)Math.Sin(Owner.Rotation)) * 15,
                typeof(Player));
            EntityManager.Add(bullet);
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            speedTimer -= timer;
            if(reloadTimer > 0) reloadTimer -= timer;
            //Console.WriteLine(Owner.Rotation);
            if (speedTimer <= 0 && reloadTimer <= 0 && ammo > 0)
                CanShoot = true;
            if (ammo <= 0 && reloadTimer <= 0)
                NeedReload = false;
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