using System;
using System.Collections.Generic;

namespace MG
{
    public interface IComponent
    {
        List<IComponent> Dependencies { get; }
        ComponentEntity Parent { get; }
    }
}