using System;
using System.Collections.Generic;

namespace MG
{
    public interface IComponent
    {
        Dictionary<Type, IComponent> Dependencies { get; }
        IComponentEntity Parent { get; }
    }
}