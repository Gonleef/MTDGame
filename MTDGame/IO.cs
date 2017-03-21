using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MG
{
	class IO
	{
		Player player;
		private Vector2 distance;

		public IO(Player newPlayer)
		{
			player = newPlayer;
		}

		public void Update(KeyboardState keyboardState, MouseState mouseState, GameTime gameTime)
		{
			distance = mouseState.Position.ToVector2() + player.PlayerCamera.Position - player.Position;

			player.Rotate((float)Math.Atan2(distance.Y, distance.X));

			if (keyboardState.IsKeyDown(Keys.Up))
			{
				Vector2 motion = new Vector2(0, -5);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
				player.Move(motion);
			}

			if (keyboardState.IsKeyDown(Keys.Down))
			{
				Vector2 motion = new Vector2(0, 5);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
				player.Move(motion);
			}

			if (keyboardState.IsKeyDown(Keys.Left))
			{
				Vector2 motion = new Vector2(-5, 0);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
				player.Move(motion);
			}

			if (keyboardState.IsKeyDown(Keys.Right))
			{
				Vector2 motion = new Vector2(5, 0);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
				player.Move(motion);
			}

		}
	}
}