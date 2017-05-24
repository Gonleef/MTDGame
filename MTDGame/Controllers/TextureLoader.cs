using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MG
{
	static class TextureLoader
	{
		public static Texture2D Player { get; private set; }
		public static Texture2D Background { get; private set; }
		public static Texture2D Building { get; private set; }
		public static Texture2D Enemy { get; private set; }
		public static Texture2D Bullet { get; private set; }
        public static Texture2D ShootingEnemy { get; private set; }
        public static Texture2D BombEnemy { get; private set; }
        public static Texture2D Pistol { get; private set; }
        public static Texture2D Shotgun { get; private set; }

        public static void LoadContent(ContentManager content)
		{
			Background = content.Load<Texture2D>("background");
			Player = content.Load<Texture2D>("player");
			Building = content.Load<Texture2D>("building");
			Enemy = content.Load<Texture2D>("enemy");
			Bullet = content.Load<Texture2D>("bullet");
            ShootingEnemy = content.Load<Texture2D>("shootingenemy");
            BombEnemy = content.Load<Texture2D>("bombenemy");
            Pistol = content.Load<Texture2D>("pistol");
            Shotgun = content.Load<Texture2D>("shotgun");
        }

	}
}
