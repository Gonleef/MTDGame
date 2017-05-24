using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MG
{
    public static class EntityManager
    {
        public static List<IComponentEntity> entities = new List<IComponentEntity>();
        public static List<IComponentEntity> enemies = new List<IComponentEntity>();

        public static void Add(IComponentEntity entity)
        {
            Game1.collisionController.Add(entity);
            entities.Add(entity);
        }

        public static void AddEnemy(IComponentEntity entity)
        {
            Game1.collisionController.Add(entity);
            enemies.Add(entity);
        }

        public static void Update(GameTime gameTime)
        {
            RemoveDeadEntities(entities);
            RemoveDeadEnemies(enemies);

            foreach (var entity in entities)
                entity.Update(gameTime);
            foreach (var entity in enemies)
                entity.Update(gameTime);

        }

        static void RemoveDeadEntities(List<IComponentEntity> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
                if (!entityList[i].Alive)
                {
                    Game1.collisionController.Remove(entityList[i]);
                    entityList.Remove(entityList[i]);
                }
        }

        static void RemoveDeadEnemies(List<IComponentEntity> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
                if (!entityList[i].Alive)
                {
                    Game1.mainPlayer.GetComponent<PlayerScore>().Increase();
                    Game1.collisionController.Remove(entityList[i]);
                    entityList.Remove(entityList[i]);
                }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);

            foreach (var entity in enemies)
                entity.Draw(spriteBatch);
        }
    }
}
