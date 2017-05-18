using System;
using System.Collections.Generic;

namespace MG
{
    public static class Inventory
    {
        static List<IItem> items = new List<IItem>();

        public static Weapon activeWeapon = new Shotgun(EntityManager.players[0]);

        public static void Add(IItem item)
        {
            items.Add(item);
        }

        public static void ActivateItem(IItem item)
        {
            if (item.GetType() == typeof(Weapon))
            {
                activeWeapon = (Weapon)item;
            }
        }

    }
}