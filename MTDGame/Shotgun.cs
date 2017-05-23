namespace MG
{
    public class Shotgun : Weapon
    {
        public Shotgun(IComponentEntity owner)
            : base(2, 0.5f, 15, 15.0f, owner)
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