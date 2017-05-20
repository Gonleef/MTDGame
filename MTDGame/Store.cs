using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MG
{
    public static class Store
    {
        static List<IItem> availableItems = new List<IItem>();
        private static int activeItem = 0;
        

        public static void WriteItem(int index)
        {

        }

        public static void PreviousItem()
        {
            activeItem--;
        }
        public static void NextItem()
        {
            activeItem++;
        }

        public static void BuyItem()
        {
            Inventory.Add(availableItems[activeItem]);
        }
    }
}
