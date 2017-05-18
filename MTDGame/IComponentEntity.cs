using System;
using System.Collections.Generic;

namespace MG
{
    public interface IComponentEntity
    {
        Dictionary<Type, IComponent> Components { get; }
        IComponent GetComponent(IComponent component);
        bool HasComponent(IComponent component);
    }
}