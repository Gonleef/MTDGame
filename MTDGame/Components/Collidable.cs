using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace MG
{
    public class Collidable : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private Rectangle _boX;

        public Collidable()
        {
            _boX = new Rectangle((int)Parent.GetComponent<Position>(typeof(Position)).position.X - (int)TextureLoader.Player.Width / 2,
                (int)Parent.GetComponent<Position>(typeof(Position)).position.Y - (int)TextureLoader.Player.Width / 2, 
                TextureLoader.Player.Width, TextureLoader.Player.Height);
        }
    }
}