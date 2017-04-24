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

        public static void Add(IEntity entity)
        {
            switch (entity.GetType().ToString())
            {
                case "MG.Player":
                    players.Add(entity);
                    break;
                case "MG.Building":
                    buildings.Add(entity);
                    break;
                case "MG.Enemy":
                    enemies.Add(entity);
                    break;
                case "MG.Bullet":
                    bullets.Add(entity);
                    break;
                case "MG.ShootingEnemy":
                    shootingEnemies.Add(entity);
                    break;
                case "MG.BombEnemy":
                    bombEnemies.Add(entity);
                    break;
            }
            Game1.collisionController.Add(entity);
        }

        public static void Update(GameTime gameTime)
        {
            RemoveDeadEntities(players);
            RemoveDeadEntities(buildings);
            RemoveDeadEntities(enemies);
            RemoveDeadEntities(bullets);
            RemoveDeadEntities(shootingEnemies);
            RemoveDeadEntities(bombEnemies);


            foreach (var player in players) player.Update(gameTime);
            foreach (var building in buildings) building.Update(gameTime);
            foreach (var enemy in enemies) enemy.Update(gameTime);
            foreach (var bullet in bullets) bullet.Update(gameTime);
            foreach (var shootingEnemy in shootingEnemies) shootingEnemy.Update(gameTime);
            foreach (var bombEnemy in bombEnemies) bombEnemy.Update(gameTime);
        }

        static void RemoveDeadEntities(List<IEntity> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                if (!entityList[i].Alive)
                {
                    Game1.collisionController.Remove(entityList[i]);
                    entityList.Remove(entityList[i]);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var player in players) if (player.Alive) player.Draw(spriteBatch);
            foreach (var building in buildings) if (building.Alive) building.Draw(spriteBatch);
            foreach (var enemy in enemies) if (enemy.Alive) enemy.Draw(spriteBatch);
            foreach (var bullet in bullets) if (bullet.Alive) bullet.Draw(spriteBatch);
            foreach (var shootingEnemy in shootingEnemies) if (shootingEnemy.Alive) shootingEnemy.Draw(spriteBatch);
            foreach (var bombEnemy in bombEnemies) if (bombEnemy.Alive) bombEnemy.Draw(spriteBatch);
        }
    }
}
