using System.Collections.Generic;
using System;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MTDGame.Components;

namespace MG
{
    public class Collidable : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public Rectangle Box;

        public Collidable(IComponentEntity Parent, Vector2 position, Texture2D texture)
        {
            this.Parent = Parent;
            Box = new Rectangle((int)position.X - (int)texture.Width / 2,
                (int)position.Y - (int)texture.Width / 2,
                texture.Width, texture.Height);
        }

        public void Update()
        {
            Box = new Rectangle((int)Parent.GetComponent<Position>().position.X - (int)Parent.GetComponent<Visible>().Texture.Width / 2,
                (int)Parent.GetComponent<Position>().position.Y - (int)Parent.GetComponent<Visible>().Texture.Width / 2,
                Parent.GetComponent<Visible>().Texture.Width, Parent.GetComponent<Visible>().Texture.Height);
        }

    }
}