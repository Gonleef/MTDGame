using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MG
{
    public class Mine : IComponentEntity
    {
        public bool Alive { get; set; }
        private float DetonateZone;
        public float DetonateTimer { get; private set; }
        private bool activate;

        public Dictionary<Type, IComponent> Components { get; private set; }
        public T GetComponent<T>()
            where T : IComponent
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

        public Mine(Vector2 startPosition)
        {
            DetonateZone = 100;
            Alive = true;
            Components = new Dictionary<Type, IComponent>
            {
                {Type.GetType("MG.Position"), new Position(this, startPosition)},
                {Type.GetType("MG.Visible"), new Visible(this, TextureLoader.Mine)}
            };
        }

        public void Collide(IComponentEntity entity)
        {

        }

        public void Update(GameTime gameTime)
        {
            if (EntityManager.enemies.Count > 0)
            {
                foreach (var enemy in EntityManager.enemies)
                {
                    if (Vector2.Distance(enemy.GetComponent<Position>().position, GetComponent<Position>().position) < DetonateZone)
                    {
                        activate = true;
                    }
                }
            }

            if (activate)
            {
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                DetonateTimer -= timer;

                if (DetonateTimer <= 0)
                    Detonate(gameTime);

            }
        }

        public void Detonate(GameTime gameTime)
        {
            foreach (var enemy in EntityManager.enemies)
            {
                if (Vector2.Distance(enemy.GetComponent<Position>().position, GetComponent<Position>().position) < DetonateZone)
                {
                    enemy.Alive = false;
                }
            }
            Alive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<Visible>().Texture, GetComponent<Position>().position, null, Color.White);
        }
    }
}
