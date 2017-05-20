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
            Vector2 motion = Vector2.Zero;

            distance = mouseState.Position.ToVector2() + player.PlayerCamera.Position - player.Position;

			player.Rotate((float)Math.Atan2(distance.Y, distance.X));

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
                player.GetComponent<HasWeapon>(typeof(HasWeapon)).Shoot();
			}

			if (keyboardState.IsKeyDown(Keys.Up))
			{
				motion = new Vector2(0, -5);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>(typeof(Movement)).Move(motion);
            }

			if (keyboardState.IsKeyDown(Keys.Down))
			{
				motion = new Vector2(0, 5);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>(typeof(Movement)).Move(motion);
            }

			if (keyboardState.IsKeyDown(Keys.Left))
			{
				motion = new Vector2(-5, 0);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>(typeof(Movement)).Move(motion);
            }

			if (keyboardState.IsKeyDown(Keys.Right))
			{
				motion = new Vector2(5, 0);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>(typeof(Movement)).Move(motion);
            }

            if (keyboardState.IsKeyDown(Keys.E))
            {
                Store.NextItem();
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                Store.NextItem();
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                Store.BuyItem();
            }
        }
	}
}