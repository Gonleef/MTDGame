using System.Collections.Generic;
using System;

namespace MG
{
    public class PlayerScore : IComponent
    {
        public Dictionary<Type, IComponent> Dependencies { get; private set; }
        public IComponentEntity Parent { get; private set; }

        public int _PlayerScore;

        public PlayerScore(IComponentEntity Parent, int baseScore)
        {
            this.Parent = Parent;
            _PlayerScore = baseScore;
        }

        public void Increase()
        {
            _PlayerScore++;
        }

    }
}