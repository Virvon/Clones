using Clones.UI;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowSecondQuestHandler : EducationHandler
    {
        private const float WaitingTime = 2;

        private readonly DialogPanel _dialogPanel;
        private readonly Waiter _waiter;
        private readonly DirectionMarker _directionMarker;
        private readonly Transform _directionMarkerTarget;

        public ShowSecondQuestHandler(DialogPanel dialogPanel, Waiter waiter, DirectionMarker directionMarker, Transform directionMarkerTarget)
        {
            _dialogPanel = dialogPanel;
            _waiter = waiter;
            _directionMarker = directionMarker;
            _directionMarkerTarget = directionMarkerTarget;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            _directionMarker.SetTarget(_directionMarkerTarget);

            _waiter.Wait(WaitingTime, callback: OnCallback);
        }

        private void OnCallback()
        {
            _dialogPanel.Close();
            Successor.Handle();
        }
    }
}
