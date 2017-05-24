﻿namespace MG
{
    public class Shotgun : Weapon
    {
        public Shotgun(IComponentEntity owner)
            : base(4, 0.5f, 15, 15.0f, owner, 10)
        {

        }

        public override void Shoot()
        {
            if (base.CanShoot)
            {
                base.Shoot();
                CreateBullet(0.2f);
                CreateBullet(-0.2f);
            }
            else if (base.NeedReload)
            {
                base.Reload();
                base.NeedReload = false;
            }
        }
    }
}