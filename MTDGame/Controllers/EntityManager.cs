using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MG
{
    public static class EntityManager
    {
        public static List<IEntity> players = new List<IEntity>();
        public static List<IEntity> buildings = new List<IEntity>();
        public static List<IEntity> enemies = new List<IEntity>();
        public static List<IEntity> bullets = new List<IEntity>();
        public static List<IEntity> shootingEnemies = new List<IEntity>();
        public static List<IEntity> bombEnemies = new List<IEntity>();

        public static List<IEntity>[] allEntities = new List<IEntity>[] { players, buildings, enemies, bullets, shootingEnemies, bombEnemies };

        private static Dictionary<Type, List<IEntity>> entityTypes = new Dictionary<Type, List<IEntity>>
        {
            {Type.GetType("MG.Player"), players},
            {Type.GetType("MG.Building"), buildings},
            {Type.GetType("MG.Enemy"), enemies},
            {Type.GetType("MG.Bullet"), bullets},
            {Type.GetType("MG.ShootingEnemy"), shootingEnemies},
            {Type.GetType("MG.BombEnemy"), bombEnemies}
        };

        public static void Add(IEntity entity)
        {
            entityTypes[entity.GetType()].Add(entity);
            Game1.collisionController.Add(entity);
        }

        public static void Update(GameTime gameTime)
        {
            RemoveDeadEntities(allEntities);
            foreach (var entity in allEntities)
                foreach (var subEntity in entity) subEntity.Update(gameTime);
        }

        static void RemoveDeadEntities(List<IEntity>[] entityList)
        {
            for (int i = 0; i < entityList.Length; i++)
                for (int j = 0; j < entityList[i].Count; j++)
                {
                    if (!entityList[i][j].Alive)
                    {
                        Game1.collisionController.Remove(entityList[i][j]);
                        entityList[i].Remove(entityList[i][j]);
                    }
                }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in allEntities)
                foreach (var subEntity in entity) subEntity.Draw(spriteBatch);
        }
    }
}
