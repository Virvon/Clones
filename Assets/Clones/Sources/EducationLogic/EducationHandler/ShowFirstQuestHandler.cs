using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowFirstQuestHandler : EducationHandler
    {
        public override void Handle()
        {
            Debug.Log("Справа есть окно с заданиями, за выполнение которых ты получишь награду");
            Successor.Handle();
        }
    }
}
