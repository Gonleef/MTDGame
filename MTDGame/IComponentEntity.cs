using System.Collections.Generic;

namespace MG
{
    public interface IComponentEntity
    {
        List<IComponent> Components { get; }
    }
}