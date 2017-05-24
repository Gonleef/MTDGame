using System.Collections.Generic;
using System;

namespace MG
{
    public class HasInventory : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private Dictionary<Type, List<IItem>> _items = new Dictionary<Type, List<IItem>>();

        public HasInventory(IComponentEntity Parent)
        {
            this.Parent = Parent;
        }

        public void Add<T>(T item) where T : IItem
        {
            _items.Add(typeof(T), new List<IItem>());
            if(!_items[typeof(T)].Contains(item))
                _items[typeof(T)].Add(item);
        }

        public void Remove<T>(T item) where T : IItem
        {
            _items[typeof(T)].Remove(item);
        }

        public void SwitchWeapon()
        {
            foreach (var weapon in _items[typeof(Weapon)])
            {
                _items[typeof(Weapon)].Add(Parent.GetComponent<HasWeapon>().Weapon);
                Parent.GetComponent<HasWeapon>().Weapon = (Weapon)weapon;
                Remove<Weapon>((Weapon)weapon);
                Console.WriteLine(_items[typeof(Weapon)].Count);
                break;

            }
        }
    }
}