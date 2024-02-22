using Clones.UI;
using System;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowFirstQuestHandler : EducationHandler
    {
        private const float WaitingTime = 3;

        private readonly DialogPanel _dialogPanel;
        private readonly Waiter _waiter;

        public ShowFirstQuestHandler(DialogPanel dialogPanel, Waiter waiter)
        {
            _dialogPanel = dialogPanel;
            _waiter = waiter;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            Debug.Log("Справа есть окно с заданиями, за выполнение которых ты получишь награду");

            _waiter.Wait(WaitingTime, callback: OnCallback);
        }

        private void OnCallback()
        {
            _dialogPanel.Close();
            Successor.Handle();
        }
    }
}
