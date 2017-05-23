using System.Collections.Generic;
using System;

namespace MG
{
    public class Transform : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public float Rotation { get; private set; }

        public Transform(IComponentEntity Parent, float rotation)
        {
            this.Parent = Parent;
            Rotation = rotation;
        }

        public void Rotate(float rotation)
        {
            Rotation = rotation;
        }
    }
}