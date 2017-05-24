using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MG
{
    public static class EntityManager
    {
        public static List<IComponentEntity> entities = new List<IComponentEntity>();

        public static void Add(IComponentEntity entity)
        {
            Game1.collisionController.Add(entity);
            entities.Add(entity);
        }

        public static void Update(GameTime gameTime)
        {
            RemoveDeadEntities(entities);
            foreach (var entity in entities)
                entity.Update(gameTime);
        }

        static void RemoveDeadEntities(List<IComponentEntity> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
                if (!entityList[i].Alive)
                {
                    if ((entityList[i].GetType() == typeof(Enemy)) || (entityList[i].GetType() == typeof(ShootingEnemy)) || (entityList[i].GetType() == typeof(BombEnemy)))
                        Game1.mainPlayer.GetComponent<PlayerScore>().Increase();
                    Game1.collisionController.Remove(entityList[i]);
                    entityList.Remove(entityList[i]);
                }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
