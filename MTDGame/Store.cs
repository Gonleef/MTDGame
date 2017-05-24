﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public class Store : IComponentEntity
    {
        public bool Alive { get; set; }
        private Vector2 shift;
        public Dictionary<Type, IComponent> Components { get; private set; }
        public T GetComponent<T>()
            where T:IComponent
        {
            if (HasComponent())
            {
                return (T)Components[typeof(T)];
            }
            return default(T);
        }

        public bool HasComponent()
        {
            return true;
        }

        public Store(Vector2 shift)
        {
            this.shift = shift;
            Components = new Dictionary<Type, IComponent>
            {
                {Type.GetType("MG.HasInventory"), new HasInventory(this)},
                {Type.GetType("MG.HasWeapon"), new HasWeapon(this, new Pistol(Game1.mainPlayer))}
            };
            GetComponent<HasInventory>().Add<Weapon>(new Shotgun(Game1.mainPlayer));
        }

        public static void BuyItem()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<HasWeapon>().Texture, Game1.mainPlayer.GetComponent<Position>().position - shift + new Vector2(120, 80), null, Color.White);
        }
    }
}
