using System.Security.Cryptography.X509Certificates;

namespace MG
{
    public class Pistol : Weapon
    {
        public Pistol(IEntity owner)
            : base(2, 0.5f, 15, 15.0f, owner)
        {

        }
    }
}