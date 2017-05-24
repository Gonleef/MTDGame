using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public class Pistol : Weapon
    {


        public Pistol(IComponentEntity owner)
            : base(2, 0.5f, 15, 15.0f, owner, 15)
        {
            base.Texture = TextureLoader.Pistol;
        }
    }
}