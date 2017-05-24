using System.Collections.Generic;
using System;

namespace MG
{
    public class Health : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public int _health;

        public Health(IComponentEntity Parent, int health)
        {
            this.Parent = Parent;
            _health = health;
        }

        public bool IsAlive()
        {
            if (_health > 0)
                return true;

            return false;
        }

        public void GetDamage(int h)
        {
            _health -= h;
            if (!IsAlive())
            {                
                Parent.Alive = false;
            }
        }

    }
}