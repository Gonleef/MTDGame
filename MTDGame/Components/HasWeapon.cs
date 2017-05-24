using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public class HasWeapon : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }
        public Texture2D Texture {get { return Weapon.Texture; }}
        public Weapon Weapon { get; set; }

        public HasWeapon(IComponentEntity Parent, Weapon weapon)
        {
            this.Parent = Parent;
            Weapon = weapon;
        }

        public void Shoot()
        {
            Weapon.Shoot();
        }

        public void Update(GameTime gameTime)
        {
            Weapon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float rotation,
            Vector2 spriteOrigin)
        {
            Weapon.Draw(spriteBatch, texture, position, rotation, spriteOrigin);
        }

    }
}