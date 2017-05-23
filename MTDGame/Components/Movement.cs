using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MG
{
    public class Movement : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public Vector2 Speed { get; private set; }

        public Movement(IComponentEntity Parent, Vector2 speed)
        {
            this.Parent = Parent;
            Speed = speed;
        }

        public void Move(Vector2 move)
        {
            this.Parent = Parent;
            Parent.GetComponent<Position>().position += Speed * move;
        }
    }
}