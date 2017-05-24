using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MG
{
    public static class UILoader
    {
        public static SpriteFont Font { get; set; }

        public static void LoadContent(ContentManager content)
        {
            Font = content.Load<SpriteFont>("spritefont");
        }
    }
}