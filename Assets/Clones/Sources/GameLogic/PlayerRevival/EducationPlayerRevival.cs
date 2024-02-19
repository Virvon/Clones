using System;

namespace Clones.GameLogic
{
    public class EducationPlayerRevival : IPlayerRevival
    {
        public bool CanRivival => true;

        public bool TryRevive(Action callback = null)
        {
            throw new NotImplementedException();
        }
    }
}
