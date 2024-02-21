using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowSecondQuestHandler : EducationHandler
    {
        public override void Handle()
        {
            Debug.Log("Выполни следующее задание и покончим с этим");
            Successor.Handle();
        }
    }
}
