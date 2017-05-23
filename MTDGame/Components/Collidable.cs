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
        public Vector2 Size { get; private set; }
        

        public Collidable(IComponentEntity Parent, Vector2 position, Texture2D texture)
        {
            this.Parent = Parent;
            Size = new Vector2(texture.Width, texture.Height);
            /*Box = new Rectangle((int)position.X - (int)texture.Width / 2,
                (int)position.Y - (int)texture.Width / 2,
                texture.Width, texture.Height);*/
            //Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.X / 2,
            //texture.Width, texture.Height);

            Box = new Rectangle((int)position.X - (int)Size.X / 2,
                (int)position.Y - (int)Size.X / 2,
                texture.Width, texture.Height);


        }

        public void Update()
        {
            Box = new Rectangle((int)Parent.GetComponent<Position>().position.X - (int)Size.X / 2,
                (int)Parent.GetComponent<Position>().position.Y - (int)Size.Y / 2,
                Parent.GetComponent<Visible>().Texture.Width, Parent.GetComponent<Visible>().Texture.Height);

            //Box = new Rectangle((int)Position.X - (int)Size.X / 2, (int)Position.Y - (int)Size.Y / 2,
            //texture.Width, texture.Height)
        }

    }
}