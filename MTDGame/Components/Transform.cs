using System.Collections.Generic;
using System;

namespace MG
{
    public class Transform : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private float _rotation;

        public Transform(float rotation)
        {
            _rotation = rotation;
        }

        public void Rotate(float rotation)
        {
            _rotation = rotation;
        }
    }
}