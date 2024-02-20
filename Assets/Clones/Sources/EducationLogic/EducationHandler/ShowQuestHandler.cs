using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowQuestHandler : EducationHandler
    {
        public override void Handle()
        {
            Debug.Log("Справа есть окно с заданиями, за выполнение которых ты получишь награду");
            Successor.Handle();
        }
    }
}
