using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public interface IComponentEntity
    {
        bool Alive { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        Dictionary<Type, IComponent> Components { get; }
        T GetComponent<T>() where T: IComponent;
        bool HasComponent();
    }
}