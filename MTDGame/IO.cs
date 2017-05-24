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
		private int ScrollValue = 0;


		public IO(Player newPlayer)
		{
			player = newPlayer;
		}

		public void Update(KeyboardState keyboardState, MouseState mouseState, GameTime gameTime)
		{
            Vector2 motion = Vector2.Zero;

            distance = mouseState.Position.ToVector2() + player.PlayerCamera.Position - player.GetComponent<Position>().position;

			player.GetComponent<Transform>().Rotate((float)Math.Atan2(distance.Y, distance.X));

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
                player.GetComponent<HasWeapon>().Shoot();
			}

			if (keyboardState.IsKeyDown(Keys.Up))
			{
				motion = new Vector2(0, -1);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>().Move(motion);
            }

			if (keyboardState.IsKeyDown(Keys.Down))
			{
				motion = new Vector2(0, 1);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>().Move(motion);
            }

			if (keyboardState.IsKeyDown(Keys.Left))
			{
				motion = new Vector2(-1, 0);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>().Move(motion);
            }

			if (keyboardState.IsKeyDown(Keys.Right))
			{
				motion = new Vector2(1, 0);
				motion *= (gameTime.ElapsedGameTime.Seconds + 1);
                player.GetComponent<Movement>().Move(motion);
            }

			if (mouseState.ScrollWheelValue != ScrollValue)
			{
				if (EnemyController.Wave)
				{
					player.GetComponent<HasInventory>().SwitchWeapon();
					ScrollValue = mouseState.ScrollWheelValue;
				}
				else
				{
					Game1.mainStore.GetComponent<HasInventory>().SwitchWeapon();
					ScrollValue = mouseState.ScrollWheelValue;
				}
			}

        }
	}
}