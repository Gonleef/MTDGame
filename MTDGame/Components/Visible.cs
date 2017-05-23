using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MG
{
    public class Visible : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public Texture2D Texture { get; private set; }


        public Visible(IComponentEntity Parent, Texture2D texture)
        {
            this.Parent = Parent;
            Texture = texture;            
        }
    }
}