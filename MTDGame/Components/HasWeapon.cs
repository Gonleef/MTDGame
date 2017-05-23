using System.Collections.Generic;
using System;

namespace MG
{
    public class HasWeapon : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private Weapon _weapon;

        public HasWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Shoot()
        {
            _weapon.Shoot();
        }
    }
}