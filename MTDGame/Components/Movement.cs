using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MG
{
    public class Movement : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public Vector2 Position { get; private set; }

        public Movement(Vector2 position)
        {
            Position = position;
        }

        public void Move(Vector2 move)
        {
            Position += move;
        }
    }
}