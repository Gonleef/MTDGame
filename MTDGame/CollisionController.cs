using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MG
{
	static class CollisionComtroller
	{
		static List<IEntity> entities = new List<IEntity>();
		static List<IEntity> players = new List<IEntity>();
		static List<Building> buildings = new List<Building>();


		public static void Add(IEntity newEntity)
		{
			switch (newEntity.GetType().ToString())
			{
				case "MG.Player":
					entities.Add(newEntity);
					players.Add(newEntity);
					break;
				case "MG.Building":
					entities.Add(newEntity);
					buildings.Add((Building)newEntity);
					break;
			}
		}

		public static void Update()
		{
			CheckCollision();
		}

		public static void CheckCollision()
		{
			
			for (int j = 0; j < entities.Count; j++)
			{
				for (int i = 0; i < entities.Count; i++)
				{
					if (entities.ToArray()[j].Box.Intersects(entities.ToArray()[i].Box))
					{
						entities.ToArray()[j].Collide(entities.ToArray()[i]);
					}
				}

			}
		}

		public static bool MoveCollision(Rectangle player)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				if(player.Intersects(entities[i].Box))
					if (!(entities[i].GetType().Name == "Player"))
					{
						Console.WriteLine(player.Bottom + " : " + entities[i].Box.Top);
						return true;
					}
			}
			return false;
		}
	

	}
}
