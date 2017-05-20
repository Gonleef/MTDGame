using System;
using System.Collections.Generic;

namespace MG
{
    public interface IComponentEntity
    {
        Dictionary<Type, IComponent> Components { get; }
        T GetComponent<T>(Type component) where T: IComponent;
        bool HasComponent(Type component);
    }
}