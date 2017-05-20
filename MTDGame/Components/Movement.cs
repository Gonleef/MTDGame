﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace MG
{
    public class Movement : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public void Move(Vector2 move)
        {
            Parent.GetComponent<Position>(typeof(Position)).position += move;
        }
    }
}