using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MG
{
    public class PlayerInfo
    {

        public void DrawHealth(SpriteBatch spriteBatch)
        {
            if (Game1.mainPlayer.GetComponent<Health>()._health != 0)
            {
                spriteBatch.DrawString(UILoader.Font, "Health: " + Game1.mainPlayer.GetComponent<Health>()._health.ToString(),
                    new Vector2(Game1.mainPlayer.GetComponent<Position>().position.X - Game1.mainPlayer.GetComponent<Visible>().Texture.Width,
                    Game1.mainPlayer.GetComponent<Position>().position.Y - Game1.mainPlayer.GetComponent<Visible>().Texture.Height * 3 / 2), Color.White);
            }
            else
            {
                spriteBatch.DrawString(UILoader.Font, "Dead",
                    new Vector2(Game1.mainPlayer.GetComponent<Position>().position.X - Game1.mainPlayer.GetComponent<Visible>().Texture.Width,
                    Game1.mainPlayer.GetComponent<Position>().position.Y - Game1.mainPlayer.GetComponent<Visible>().Texture.Height * 3 / 2), Color.White);
            }
        }

        public void DrawScore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(UILoader.Font, "Score: " + Game1.mainPlayer.GetComponent<PlayerScore>()._PlayerScore.ToString(),
                new Vector2(Game1.mainPlayer.GetComponent<Position>().position.X - Game1.mainPlayer.GetComponent<Visible>().Texture.Width,
                Game1.mainPlayer.GetComponent<Position>().position.Y + Game1.mainPlayer.GetComponent<Visible>().Texture.Height * 3 / 2), Color.White);

        }


    }
}
