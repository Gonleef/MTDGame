﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	static class EnemyController
	{

		public static void Update(GameTime gameTime)
		{
		}

		public static void CreateEnemey()
		{
			Random position = new Random();
			Enemy newEnemy = new Enemy(new Vector2(position.Next(50, 1200), position.Next(50 ,1200)));
			EntityManager.Add(newEnemy);
		}

        public static void CreateShootingEnemy()
        {
            Random position = new Random();
            ShootingEnemy newShootingEnemy = new ShootingEnemy(new Vector2(position.Next(50, 1200), position.Next(50, 1200)));
            EntityManager.Add(newShootingEnemy);
        }

        public static void CreateBombEnemy()
        {
            Random position = new Random();
            BombEnemy newBombEnemy = new BombEnemy(new Vector2(position.Next(50, 1200), position.Next(50, 1200)));
            EntityManager.Add(newBombEnemy);
        }
    }
}
