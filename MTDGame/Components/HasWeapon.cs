using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace MG
{
    public class HasWeapon : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private Weapon _weapon;

        public HasWeapon(IComponentEntity Parent, Weapon weapon)
        {
            this.Parent = Parent;
            _weapon = weapon;
        }

        public void Shoot()
        {
            _weapon.Shoot();
        }

        public void Update(GameTime gameTime)
        {
            _weapon.Update(gameTime);
        }

    }
}