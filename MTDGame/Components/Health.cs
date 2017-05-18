﻿using System.Collections.Generic;
using System;

namespace MG
{
    public class Health : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private int _health;

        public Health(int health)
        {
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
        }

    }
}