using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MG
{
    public class Position : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public Vector2 position { get; set; }

        public Position(Vector2 position)
        {
            this.position = position;
        }
    }
}