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

        public Position(IComponentEntity Parent, Vector2 position)
        {
            this.Parent = Parent;
            this.position = position;
        }
    }
}