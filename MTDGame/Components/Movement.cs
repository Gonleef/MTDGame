using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MG
{
    public class Movement
    {
        public List<IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private Vector2 _position;

        public Movement(Vector2 position)
        {
            _position = position;
        }

        public void Move(Vector2 move)
        {
            _position += move;
        }
    }
}