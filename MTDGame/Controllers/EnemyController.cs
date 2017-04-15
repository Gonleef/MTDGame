using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
	static class EnemyController
	{

		public static void Update(GameTime gameTime)
		{
			if (EntityManager.enemies.Count < 5)
				CreateEnemey();
			foreach (var enemy in EntityManager.enemies)
			{
				enemy.Update(gameTime);
			}
		}

		public static void CreateEnemey()
		{
			Random position = new Random();
			Enemy newEnemy = new Enemy(new Vector2(position.Next(50, 1200), position.Next(50 ,1200)));
			EntityManager.Add(newEnemy);
		}
	}
}
