using Clones.UI;
using System;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowFirstQuestHandler : EducationHandler
    {
        private const float WaitingTime = 5;

        private readonly DialogPanel _dialogPanel;
        private readonly Waiter _waiter;
        private readonly FrameFocus _frameFocus;

        public ShowFirstQuestHandler(DialogPanel dialogPanel, Waiter waiter, FrameFocus frameFocus)
        {
            _dialogPanel = dialogPanel;
            _waiter = waiter;
            _frameFocus = frameFocus;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            _frameFocus.Open();
            Debug.Log("Справа есть окно с заданиями, за выполнение которых ты получишь награду");

            _waiter.Wait(WaitingTime, callback: OnCallback);
        }

        private void OnCallback()
        {
            _frameFocus.Close();
            _dialogPanel.Close();
            Successor.Handle();
        }
    }
}
