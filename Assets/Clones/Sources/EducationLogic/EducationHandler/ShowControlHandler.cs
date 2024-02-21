using Clones.GameLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.EducationLogic
{

    public class ShowControlHandler : EducationHandler
    {
        private readonly IInputService _inputService;

        public ShowControlHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Handle()
        {
            Debug.Log("Проведите мышью или пальцем по экрану для движения");
            _inputService.Activated += OnInputActivated;
        }

        private void OnInputActivated()
        {
            _inputService.Activated -= OnInputActivated;
            Successor.Handle();
        }
    }
}
