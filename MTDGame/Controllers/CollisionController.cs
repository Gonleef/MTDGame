using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MG
{
	static class CollisionComtroller
	{
		public static void Update()
		{
			CheckCollision(EntityManager.bullets, EntityManager.buildings);
			CheckCollision(EntityManager.bullets, EntityManager.enemies);
			CheckCollision(EntityManager.players, EntityManager.buildings);
			CheckCollision(EntityManager.players, EntityManager.enemies);
			CheckCollision(EntityManager.enemies, EntityManager.enemies);
			CheckCollision(EntityManager.enemies, EntityManager.buildings);

		}


		public static void CheckCollision(List<IEntity> entityList1, List<IEntity> entityList2)
		{
			for (int i = 0; i < entityList1.Count; i++)
				for (int j = 0; j < entityList2.Count; j++)
				{
					if (entityList1[i].Box.Intersects(entityList2[j].Box))
					{
						entityList1[i].Collide(entityList2[j]);
						entityList2[j].Collide(entityList1[i]);
					}
				}
		}


	}
}
