using System.Collections.Generic;
using System;

namespace MG
{
    public class HasInventory : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        private List<IItem> _items;

        public HasInventory(IComponentEntity Parent)
        {
            this.Parent = Parent;
        }

        public void Add(IItem item)
        {
            _items.Add(item);
        }

        public void Remove(IItem item)
        {
            _items.Remove(item);
        }

        public bool HasItem(IItem item)
        {
            if (_items.Contains(item)) return true;

            return false;
        }
    }
}