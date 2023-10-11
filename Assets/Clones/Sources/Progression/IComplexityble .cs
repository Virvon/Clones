using System;

namespace Clones.Progression
{
    public interface IComplexityble
    {
        public event Action ComplexityIncreased;

        public int QuestLevel { get; }
    }
}
