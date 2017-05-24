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
        SpriteFont font;
        public PlayerInfo(ContentManager content)
        {
            font = content.Load<SpriteFont>("spritefont");
        }

        public void DrawHealth(SpriteBatch spriteBatch)
        {
            if (Game1.mainPlayer.GetComponent<Health>()._health != 0)
            {
                spriteBatch.DrawString(font, "Health: " + Game1.mainPlayer.GetComponent<Health>()._health.ToString(),
                    new Vector2(Game1.mainPlayer.GetComponent<Position>().position.X - Game1.mainPlayer.GetComponent<Visible>().Texture.Width,
                    Game1.mainPlayer.GetComponent<Position>().position.Y - Game1.mainPlayer.GetComponent<Visible>().Texture.Height * 3 / 2), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, "Dead",
                    new Vector2(Game1.mainPlayer.GetComponent<Position>().position.X - Game1.mainPlayer.GetComponent<Visible>().Texture.Width,
                    Game1.mainPlayer.GetComponent<Position>().position.Y - Game1.mainPlayer.GetComponent<Visible>().Texture.Height * 3 / 2), Color.White);
            }
        }

        public void DrawScore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + Game1.mainPlayer.GetComponent<PlayerScore>()._PlayerScore.ToString(),
                new Vector2(Game1.mainPlayer.GetComponent<Position>().position.X - Game1.mainPlayer.GetComponent<Visible>().Texture.Width,
                Game1.mainPlayer.GetComponent<Position>().position.Y + Game1.mainPlayer.GetComponent<Visible>().Texture.Height * 3 / 2), Color.White);

        }
    }
}
